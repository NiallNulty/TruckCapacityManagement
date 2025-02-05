using System;
using System.Collections.Generic;
using System.Linq;
using TruckCapacityManagement.Controllers;
using TruckCapacityManagement.Models;

namespace TruckCapacityManagement.Utilities
{
    public class DataModifier
    {
        /// <summary>
        /// Calls all methods required to attempt to create new order lists.
        /// </summary>
        public static List<List<Order>> TryCreateNewOrderLists(List<Order> orders, decimal truckCapacity, decimal maximumReductionPercentage)
        {
            List<List<Order>> newOrders = new List<List<Order>>();

            UIController.GetErrorMessage();

            // If there is enough space on the truck for all the orders, let the user know //
            if (ThereIsEnoughCapacityOnTruck(orders.Sum(order => order.OrderQty), truckCapacity))
            {
                UIController.SetErrorMessage("All orders in file should fit in delivery, no need to generate new files.");
                return newOrders;
            }

            // Truck Capacity must be greater than 0 //
            if (truckCapacity == 0)
            {
                UIController.SetErrorMessage("Truck Capacity must be greater than 0");
                return newOrders;
            }

            // Maximum Reduction Percentage must be greater than 0 //
            if (maximumReductionPercentage == 0)
            {
                UIController.SetErrorMessage("Maximum Reduction Percentage must be greater than 0.");
                return newOrders;
            }

            // Get the first order //
            List<Order> firstOrder = GetFirstOrder(orders, truckCapacity, maximumReductionPercentage);

            // If the order could not be scaled back, there is no point attempting to scale it back again //
            if (firstOrder.Count == 0 || firstOrder.Sum(order => order.DeliveryQty) == 0)
            {
                UIController.SetErrorMessage("Order could not be scaled back with the given Truck Capacity and Reduction Percentage.");
                return newOrders;
            }

            // If we can successfuly scale back the first order, try create a second order //
            List<Order> secondOrder = GetSecondOrder(firstOrder, truckCapacity, maximumReductionPercentage);

            // Return both orders //
            newOrders.Add(firstOrder);
            newOrders.Add(secondOrder);
            return newOrders;          
        }

        /// <summary>
        /// Checks to see if the delivery will fit on the truck. Returns true if it will fit. Returns false if it won't.
        /// </summary>
        /// <returns></returns>
        private static bool ThereIsEnoughCapacityOnTruck(int orderQty, decimal truckCapacity)
        {
            if (orderQty <= truckCapacity)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Responsible for calculating how much to reduce the Order Quantities by for the first delivery.
        /// </summary>
        /// <returns></returns>
        private static List<Order> GetFirstOrder(List<Order> orders, decimal truckCapacity, decimal maximumReductionPercentage)
        {
            // First get the order quantity to meet the capacity of the truck //
            List<Order> firstOrder = new List<Order>();

            try
            {
                // Get the total orders divided by total capacity //
                decimal scaleFactor = orders.Sum(order => order.OrderQty) / truckCapacity;

                // divide by the scale factor to get an even amount for the delivery //
                // and also set the minimum scaled amount allowed to ensure it doesn't go under the minimum amount allowed in a delivery //
                foreach (Order order in orders)
                {
                    Order scaledOrder = new Order
                    {
                        Customer = order.Customer,
                        OrderNumber = order.OrderNumber,
                        ProductCode = order.ProductCode,
                        OrderQty = order.OrderQty,
                        ScaledOrderQty = Convert.ToInt32(Math.Round(order.OrderQty / scaleFactor, 0)),
                        MinimumScaledOrderQtyAllowed = Convert.ToInt32(Math.Round(order.OrderQty / maximumReductionPercentage, 0)),
                        RemainingQty = order.OrderQty
                    };

                    // If the Scaled Order Quantity is under the minimum amount allowed, it can't be delivered, so the amount to delivery amount should be 0 //
                    if (scaledOrder.ScaledOrderQty >= scaledOrder.MinimumScaledOrderQtyAllowed)
                    {
                        scaledOrder.DeliveryQty = scaledOrder.ScaledOrderQty;
                        scaledOrder.RemainingQty = scaledOrder.OrderQty - scaledOrder.ScaledOrderQty;
                    }

                    firstOrder.Add(scaledOrder);
                }
            }
            catch (Exception ex)
            {
                UIController.SetErrorMessage(ex.Message);
            }

            return firstOrder;
        }

        private static List<Order> GetSecondOrder(List<Order> firstOrder, decimal truckCapacity, decimal maximumReductionPercentage)
        {
            // First get the order quantity to meet the capacity of the truck //
            List<Order> secondOrder = new List<Order>();

            try
            {
                // First check to see if the remaining can fit on the next delivery //
                if (ThereIsEnoughCapacityOnTruck(firstOrder.Sum(order => order.RemainingQty), truckCapacity))
                {
                    foreach (Order order in firstOrder)
                    {
                        Order scaledOrder = new Order
                        {
                            Customer = order.Customer,
                            OrderNumber = order.OrderNumber,
                            ProductCode = order.ProductCode,
                            OrderQty = order.OrderQty,
                            ScaledOrderQty = order.RemainingQty,
                            MinimumScaledOrderQtyAllowed = Convert.ToInt32(Math.Round(order.RemainingQty / maximumReductionPercentage, 0)), // This may be incorrect if the percentage is supposed to be based on the Original Order Qty and not the Remaining Qty //
                            RemainingQty = 0
                        };

                        // If the Scaled Order Quantity is under the minimum amount allowed, it can't be delivered, so the amount to delivery amount should be 0 //
                        if (scaledOrder.ScaledOrderQty >= scaledOrder.MinimumScaledOrderQtyAllowed)
                        {
                            scaledOrder.DeliveryQty = scaledOrder.ScaledOrderQty;
                        }

                        secondOrder.Add(scaledOrder);
                    }

                    // If there is enough to fit the capacity of the truck, we can just deliver the remaining //
                    // As long as the qty isn't lower than the minimum allowed //
                    return secondOrder;
                }

                // The total quantity is more than the truck capacity, therefore we need to apply the same scaling logic as before //

                // Get the total orders divided by total capacity //
                decimal scaleFactor = firstOrder.Sum(order => order.RemainingQty) / maximumReductionPercentage;

                // divide by the scale factor to get an even amount for the delivery //
                // and also set the minimum scaled amount allowed to ensure it doesn't go under the minimum amount allowed in a delivery //
                foreach (Order order in firstOrder)
                {
                    Order scaledOrder = new Order
                    {
                        Customer = order.Customer,
                        OrderNumber = order.OrderNumber,
                        ProductCode = order.ProductCode,
                        OrderQty = order.OrderQty,
                        ScaledOrderQty = Convert.ToInt32(Math.Round(order.RemainingQty / scaleFactor, 0)),
                        MinimumScaledOrderQtyAllowed = Convert.ToInt32(Math.Round(order.RemainingQty / maximumReductionPercentage, 0)), // This may be incorrect if the percentage is supposed to be based on the Original Order Qty and not the Remaining Qty //
                        RemainingQty = order.RemainingQty
                    };

                    // If the Scaled Order Quantity is under the minimum amount allowed, it can't be delivered, so the amount to delivery amount should be 0 //
                    if (scaledOrder.ScaledOrderQty >= scaledOrder.MinimumScaledOrderQtyAllowed)
                    {
                        scaledOrder.DeliveryQty = scaledOrder.ScaledOrderQty;
                        scaledOrder.RemainingQty -= scaledOrder.ScaledOrderQty;
                    }

                    secondOrder.Add(scaledOrder);
                }
            }
            catch (Exception ex)
            {
                UIController.SetErrorMessage(ex.Message);
            }

            return secondOrder;
        }
    }
}
