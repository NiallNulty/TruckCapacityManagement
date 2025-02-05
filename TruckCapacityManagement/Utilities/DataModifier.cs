using System;
using System.Collections.Generic;
using System.Linq;
using TruckCapacityManagement.Constants;
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

            // Maximum Reduction Percentage must be greater than 0 and less than 100 //
            if (maximumReductionPercentage == 0 || maximumReductionPercentage > 100)
            {
                UIController.SetErrorMessage("Maximum Reduction Percentage must be greater than 0 and greater than 100.");
                return newOrders;
            }

            for (int i = 0; i < AppConstants.NumberOfOrderFilesToCreate; i++)
            {
                List<Order> scaledOrders = new List<Order>();
                
                // it's the first order //
                if (i == 0)
                {
                    scaledOrders = GetScaledOrders(orders, truckCapacity, maximumReductionPercentage, true);

                }
                // it's a subsequent order //
                else
                {
                    scaledOrders = GetScaledOrders(newOrders[i-1], truckCapacity, maximumReductionPercentage, false);
                }

                // if successfully scaled, add it to list of new orders otherwise stop trying to scale subsequent orders //
                if (scaledOrders.Count > 0)
                {
                    newOrders.Add(scaledOrders);
                }
                // otherwise stop trying to scale subsequent orders //
                else
                {
                    UIController.SetMessageBoxText("Could not scale order with supplied Truck Capacity & Reduction Percentage. Please try again with different values.","Warnings");
                    break;
                }
            }

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

        private static List<Order> GetScaledOrders(List<Order> orders, decimal truckCapacity, decimal maximumReductionPercentage, bool firstOrder)
        {
            // First get the order quantity to meet the capacity of the truck //
            List<Order> scaledOrders = new List<Order>();

            try
            {
                // Get the sum of OrderQty //
                int orderQtySum = orders.Sum(order => firstOrder ? order.OrderQty : order.RemainingQty);

                if (orderQtySum == 0)
                {
                    return scaledOrders;
                }

                // The total quantity is more than the truck capacity, therefore we need to apply scaling logic //

                // Get the total orders divided by total capacity //
                decimal scaleFactor = orderQtySum / truckCapacity;

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
                        ScaledOrderQty = firstOrder ? Convert.ToInt32(Math.Round(order.OrderQty / scaleFactor, 0)) : Convert.ToInt32(Math.Round(order.RemainingQty / scaleFactor, 0)),
                        MinimumScaledOrderQtyAllowed = firstOrder ? Convert.ToInt32(Math.Round(order.OrderQty / maximumReductionPercentage, 0)) : Convert.ToInt32(Math.Round(order.RemainingQty / maximumReductionPercentage, 0)), // This may be incorrect if the percentage is supposed to be based on the Original Order Qty and not the Remaining Qty //
                        RemainingQty = firstOrder ? order.OrderQty : order.RemainingQty
                    };

                    // If the Scaled Order Quantity is under the minimum amount allowed, it can't be delivered, so the amount to deliver should be 0 //
                    if (scaledOrder.ScaledOrderQty >= scaledOrder.MinimumScaledOrderQtyAllowed)
                    {
                        scaledOrder.DeliveryQty = scaledOrder.RemainingQty < scaledOrder.ScaledOrderQty ? scaledOrder.RemainingQty : scaledOrder.ScaledOrderQty;
                        scaledOrder.RemainingQty = firstOrder ? scaledOrder.OrderQty - scaledOrder.ScaledOrderQty : scaledOrder.RemainingQty - scaledOrder.ScaledOrderQty;
                    }

                    scaledOrders.Add(scaledOrder);
                }
            }
            catch (Exception ex)
            {
                UIController.SetErrorMessage(ex.Message);
            }

            return scaledOrders;
        }
    }
}
