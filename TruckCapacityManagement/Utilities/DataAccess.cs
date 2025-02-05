using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TruckCapacityManagement.Constants;
using TruckCapacityManagement.Controllers;
using TruckCapacityManagement.Models;

namespace TruckCapacityManagement.Utilities
{
    public class DataAccess
    {
        private static DateTime orderDate;

        /// <summary>
        /// Calls all methods required to attempt to read the selected CSV file.
        /// </summary>
        /// <param name="openFileDialog"></param>
        public static List<Order> TryReadFile(OpenFileDialog openFileDialog)
        {
            // Object to return //
            List<Order> orders = new List<Order>();

            // If the file name is invalid, don't go any further //
            if (!IsValidFileName(Path.GetFileNameWithoutExtension(openFileDialog.FileName)))
            {
                UIController.SetErrorMessage($"Invalid file name. Please use the naming convention [{AppConstants.OrderFileNamePrefix}-{AppConstants.DateFormatYYYYMMDD.ToUpper()}].");
                UIController.SetUIEnabledValue(false);
                return orders;
            }

            // If the file format is invalid, don't go any further //
            if (!IsValidFileFormat(openFileDialog.FileName))
            {
                UIController.SetErrorMessage($"Headings missing from file. Please ensure the following headings exist in order: {nameof(Order.Customer)}, {nameof(Order.OrderNumber)}, {nameof(Order.ProductCode)}, {nameof(Order.OrderQty)}");
                UIController.SetUIEnabledValue(false);
                return orders;
            }

            // Create a list of orders in memory for data manipualtion later //
            orders = CreateOrdersList(openFileDialog.FileName);

            // Let the user know what file they selected & how many orders were found in the file //
            UIController.SetSelectedFileName($"Selected File: {Path.GetFileNameWithoutExtension(openFileDialog.FileName)}");
            UIController.SetOrdersFoundCountText($"{orders.Count} Order(s) Found");

            // Only allow the user to progress futher if there is orders found //
            if (orders.Count > 0)
            {
                UIController.SetUIEnabledValue(true);
            }
            else
            {
                UIController.SetUIEnabledValue(false);
            }

            return orders;
        }

        /// <summary>
        /// Validates the file name. Returns true if file name is valid. Returns false if file name is invalid.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static bool IsValidFileName(string fileName)
        {
            // Example file naming convention //
            // orders-YYYYMMDD //

            // The name must be 15 characters in length, otherwise, there are additional characters in the file name
            if (fileName.Length != 15)
            {
                return false;
            }

            // The name must start with orders- //
            if (!fileName.StartsWith(AppConstants.OrderFileNamePrefix))
            {
                return false;
            }

            // The name must end with a valid date in format YYYYMMDD // 
            bool isValidDate = DateTime.TryParseExact(
                fileName.Substring(fileName.Length - 8),
                AppConstants.DateFormatYYYYMMDD,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out orderDate
            );

            // If the date is invalid then the file name is invalid //
            if (!isValidDate)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates the file format. Returns true if file format is valid. Returns false if file format is invalid.
        /// </summary>
        /// <param name="fileNameWithPath"></param>
        /// <returns></returns>
        private static bool IsValidFileFormat(string fileNameWithPath)
        {
            if (File.Exists(fileNameWithPath))
            {
                if (new FileInfo(fileNameWithPath).Length == 0)
                {
                    return false;
                }

                StreamReader streamReader = new StreamReader(File.OpenRead(fileNameWithPath));

                string[] headers = streamReader.ReadLine().Split(',');

                streamReader.Close();

                if (headers.Length == 4 &&
                    headers[0] == nameof(Order.Customer) &&
                    headers[1] == nameof(Order.OrderNumber) &&
                    headers[2] == nameof(Order.ProductCode) &&
                    headers[3] == nameof(Order.OrderQty))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Populates orders, which is a List of Order objects, with data from the CSV.
        /// </summary>
        /// <param name="fileNameWithPath"></param>
        private static List<Order> CreateOrdersList(string fileNameWithPath)
        {
            // Object to return //
            List<Order> orders = new List<Order>();

            try
            {
                if (File.Exists(fileNameWithPath))
                {
                    foreach (string line in File.ReadLines(fileNameWithPath).Skip(1))
                    {
                        string[] columnValues = line.Split(',');

                        Order order = new Order
                        {
                            Customer = Convert.ToInt32(columnValues[0]),
                            OrderNumber = Convert.ToInt32(columnValues[1]),
                            ProductCode = columnValues[2],
                            OrderQty = Convert.ToInt32(columnValues[3]),
                        };

                        orders.Add(order);
                    }
                }
            }
            catch (Exception ex)
            {
                UIController.SetErrorMessage(ex.Message);
            }

            return orders;
        }

        /// <summary>
        /// Gets the original order date.
        /// </summary>
        /// <returns></returns>
        public static DateTime GetOrderDate()
        {
            return orderDate;
        }
    }
}
