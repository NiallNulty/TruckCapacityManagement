using System;
using System.Collections.Generic;
using System.IO;
using TruckCapacityManagement.Constants;
using TruckCapacityManagement.Controllers;
using TruckCapacityManagement.Models;

namespace TruckCapacityManagement.Utilities
{
    public class FileCreation
    {
        public static void CreateNewOrderFiles(DateTime orderDate, List<Order> firstOrder, List<Order> secondOrder)
        {
            try
            {
                // Store the new files in a subfolder so they don't overwrite the original order file //
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + AppConstants.NewOrdersFolderName);

                // The original file name remains for the first file // 
                string firstOrderFileName = $"{AppConstants.OrderFileNamePrefix}{orderDate.ToString(AppConstants.DateFormatYYYYMMDD)}{AppConstants.CSVFileFormat}";

                // The second file is the same name +1 day //
                string secondOrderFileName = $"{AppConstants.OrderFileNamePrefix}{orderDate.AddDays(1).ToString(AppConstants.DateFormatYYYYMMDD)}{AppConstants.CSVFileFormat}";

                // Write the first order to a csv //
                using (StreamWriter streamWriter = new StreamWriter($"{AppConstants.NewOrdersFolderName}/{firstOrderFileName}"))
                {
                    streamWriter.WriteLine($"{nameof(Order.Customer)},{nameof(Order.OrderNumber)},{nameof(Order.ProductCode)},{nameof(Order.OrderQty)}");

                    foreach (Order order in firstOrder)
                    {
                        streamWriter.WriteLine($"{order.Customer},{order.OrderNumber},{order.ProductCode},{order.DeliveryQty}");
                    }
                }

                // Write the second order to a csv //
                using (StreamWriter streamWriter = new StreamWriter($"{AppConstants.NewOrdersFolderName}/{secondOrderFileName}"))
                {
                    streamWriter.WriteLine($"{nameof(Order.Customer)},{nameof(Order.OrderNumber)},{nameof(Order.ProductCode)},{nameof(Order.OrderQty)}");

                    foreach (Order order in secondOrder)
                    {
                        streamWriter.WriteLine($"{order.Customer},{order.OrderNumber},{order.ProductCode},{order.DeliveryQty}");
                    }
                }

                // Let the user know the outcome //
                if (File.Exists($"{AppConstants.NewOrdersFolderName}/{firstOrderFileName}")  && File.Exists($"{AppConstants.NewOrdersFolderName}/{secondOrderFileName}"))
                {
                    UIController.SetMessageBoxText($"{firstOrderFileName} & {secondOrderFileName} were successfully created in directory: \n\n{AppDomain.CurrentDomain.BaseDirectory + AppConstants.NewOrdersFolderName}", "Success!");
                }
                else
                {
                    UIController.SetMessageBoxText("Order files were not created.", "Warning!");
                }
            }
            catch (Exception ex)
            {
                UIController.SetErrorMessage(ex.Message);
            }
        }
    }
}
