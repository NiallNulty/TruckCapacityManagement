using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TruckCapacityManagement.Models;

namespace TruckCapacityManagement
{
    public partial class MainForm : Form
    {
        private List<Order> orders = new List<Order>();
        private DateTime orderDate;

        public MainForm()
        {
            InitializeComponent();
            SetDefaultLabelTextValues();
        }

        #region Button Presses

        /// <summary>
        /// Handles logic for presing btnOpenFile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                TryReadFile(openFileDialog);
            }
        }

        /// <summary>Handles logic for presing btnGenerateNewOrders.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateNewOrders_Click(object sender, EventArgs e)
        {
            TryCreateNewOrderFiles();
        }

        #endregion

        #region Reading CSV

        /// <summary>
        /// Calls all methods required to attempt to read the selected CSV file.
        /// </summary>
        /// <param name="openFileDialog"></param>
        private void TryReadFile(OpenFileDialog openFileDialog)
        {
            // Clear the messages ever time the users tries to read a new file //
            lblSelectedFileName.Text = string.Empty;
            lblOrdersFoundCount.Text = string.Empty;
            lblErrorMessage.Text = string.Empty;

            // If the file name is invalid, don't go any further //
            if (!IsValidFileName(Path.GetFileNameWithoutExtension(openFileDialog.FileName)))
            {
                lblErrorMessage.Text = "Invalid file name. Please use the naming convention [orders-YYYYMMDD].";
                ChangeStateOfRulesUI(false);
                return;
            }

            // If the file format is invalid, don't go any further //
            if (!IsValidFileFormat(openFileDialog.FileName))
            {
                lblErrorMessage.Text = $"Headings missing from file. Please ensure the following headings exist in order: {nameof(Order.Customer)}, {nameof(Order.OrderNumber)}, {nameof(Order.ProductCode)}, {nameof(Order.OrderQty)}";
                ChangeStateOfRulesUI(false);
                return;
            }

            // Create a list of orders in memory for data manipualtion later //
            CreateOrdersList(openFileDialog.FileName);

            // Let the user know what file they selected & how many orders were found in the file //
            lblSelectedFileName.Text = $"Selected File: {Path.GetFileNameWithoutExtension(openFileDialog.FileName)}";
            lblOrdersFoundCount.Text = $"{orders.Count} Order(s) Found";

            // Only allow the user to progress futher if there is orders found //
            if (orders.Count > 0)
            {
                ChangeStateOfRulesUI(true);
            }
            else
            {
                ChangeStateOfRulesUI(false);
            }
        }

        /// <summary>
        /// Validates the file name. Returns true if file name is valid. Returns false if file name is invalid.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool IsValidFileName(string fileName)
        {
            // Example file naming convention //
            // orders-YYYYMMDD //

            // The name must be 15 characters in length, otherwise, there are additional characters in the file name
            if (fileName.Length != 15)
            {
                return false;
            }

            // The name must start with orders- //
            if (!fileName.StartsWith("orders-"))
            {
                return false;
            }

            // The name must end with a valid date in format YYYYMMDD // 
            bool isValidDate = DateTime.TryParseExact(
                fileName.Substring(fileName.Length - 8),
                "yyyyMMdd",
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
        private bool IsValidFileFormat(string fileNameWithPath)
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
        private void CreateOrdersList(string fileNameWithPath)
        {
            orders.Clear();

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
                lblErrorMessage.Text = ex.Message;
            }
        }

        #endregion

        #region Rules

        /// <summary>
        /// Calls all methods required to attempt to create new order files.
        /// </summary>
        private void TryCreateNewOrderFiles()
        {
            lblErrorMessage.Text = string.Empty;

            // If there is enough space on the truck for all the orders, let the user know //
            if (ThereIsEnoughCapacityOnTruck(orders.Sum(order => order.OrderQty)))
            {
                lblErrorMessage.Text = "All orders in file should fit in delivery, no need to generate new files.";
                return;
            }

            // Truck Capacity must be greater than 0 //
            if (numericUpDownTruckCapacity.Value == 0)
            {
                lblErrorMessage.Text = "Truck Capacity must be greater than 0";
                return;
            }

            // Maximum Reduction Percentage must be greater than 0 //
            if (numericUpDownMaximumReductionPercentage.Value == 0)
            {
                lblErrorMessage.Text = "Maximum Reduction Percentage must be greater than 0.";
                return;
            }

            // Get the first order //
            List<Order> firstOrder = GetFirstOrder();

            // If the order could not be scaled back, there is no point attempting to scale it back again //
            if(firstOrder.Count == 0 || firstOrder.Sum(order => order.DeliveryQty) == 0)
            {
                lblErrorMessage.Text = "Order could not be scaled back with the given Truck Capacity and Reduction Percentage.";
                return;
            }

            // If we can successfuly scale back the first order, try create a second order //
            List<Order> secondOrder = GetSecondOrder(firstOrder);

            // Create the two files //
            CreateNewOrderFiles(firstOrder, secondOrder);
        }

        /// <summary>
        /// Checks to see if the delivery will fit on the truck. Returns true if it will fit. Returns false if it won't.
        /// </summary>
        /// <returns></returns>
        private bool ThereIsEnoughCapacityOnTruck(int orderQty)
        {
            if (orderQty <= numericUpDownTruckCapacity.Value)
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
        private List<Order> GetFirstOrder()
        {
            // First get the order quantity to meet the capacity of the truck //
            List<Order> firstOrder = new List<Order>();

            try
            {
                // Get the total orders divided by total capacity //
                decimal scaleFactor = orders.Sum(order => order.OrderQty) / numericUpDownTruckCapacity.Value;

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
                        MinimumScaledOrderQtyAllowed = Convert.ToInt32(Math.Round(order.OrderQty / numericUpDownMaximumReductionPercentage.Value, 0)), 
                        RemainingQty = order.OrderQty
                    };

                    // If the Scaled Order Quantity is under the minimum amount allowed, it can't be delivered, so the amount to delivery amount should be 0 //
                    if(scaledOrder.ScaledOrderQty >= scaledOrder.MinimumScaledOrderQtyAllowed)
                    {
                        scaledOrder.DeliveryQty = scaledOrder.ScaledOrderQty;
                        scaledOrder.RemainingQty = scaledOrder.OrderQty - scaledOrder.ScaledOrderQty;
                    }

                    firstOrder.Add(scaledOrder);
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
            }

            return firstOrder;
        }

        private List<Order> GetSecondOrder(List<Order> firstOrder)
        {
            // First get the order quantity to meet the capacity of the truck //
            List<Order> secondOrder = new List<Order>();
            
            try
            {
                // First check to see if the remaining can fit on the next delivery //
                if (ThereIsEnoughCapacityOnTruck(firstOrder.Sum(order => order.RemainingQty)))
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
                            MinimumScaledOrderQtyAllowed = Convert.ToInt32(Math.Round(order.RemainingQty / numericUpDownMaximumReductionPercentage.Value, 0)), // This may be incorrect if the percentage is supposed to be based on the Original Order Qty and not the Remaining Qty //
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
                decimal scaleFactor = firstOrder.Sum(order => order.RemainingQty) / numericUpDownTruckCapacity.Value;

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
                        MinimumScaledOrderQtyAllowed = Convert.ToInt32(Math.Round(order.RemainingQty / numericUpDownMaximumReductionPercentage.Value, 0)), // This may be incorrect if the percentage is supposed to be based on the Original Order Qty and not the Remaining Qty //
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
                lblErrorMessage.Text = ex.Message;
            }

            return secondOrder;
        }

        #endregion

        #region File Creation

        private void CreateNewOrderFiles(List<Order> firstOrder, List<Order> secondOrder)
        {
            try
            {
                // Store the new files in a subfolder so they don't overwrite the original order file //
                string folderName = "NewOrders";
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + folderName);

                // The original file name remains for the first file // 
                string firstOrderFileName = $"orders-{orderDate.ToString("yyyyMMdd")}.csv";

                // The second file is the same name +1 day //
                string secondOrderFileName = $"orders-{orderDate.AddDays(1).ToString("yyyyMMdd")}.csv";

                // Write the first file to a csv //
                using (StreamWriter streamWriter = new StreamWriter($"{folderName}/{firstOrderFileName}"))
                {
                    streamWriter.WriteLine($"{nameof(Order.Customer)},{nameof(Order.OrderNumber)},{nameof(Order.ProductCode)},{nameof(Order.OrderQty)}");

                    foreach (Order order in firstOrder)
                    {
                        streamWriter.WriteLine($"{order.Customer},{order.OrderNumber},{order.ProductCode},{order.DeliveryQty}");
                    }
                }

                // Write the first file to a csv //
                using (StreamWriter streamWriter = new StreamWriter($"{folderName}/{secondOrderFileName}"))
                {
                    streamWriter.WriteLine($"{nameof(Order.Customer)},{nameof(Order.OrderNumber)},{nameof(Order.ProductCode)},{nameof(Order.OrderQty)}");

                    foreach (Order order in secondOrder)
                    {
                        streamWriter.WriteLine($"{order.Customer},{order.OrderNumber},{order.ProductCode},{order.DeliveryQty}");
                    }
                }

                // Let the user know the files were created successfully and their location //
                MessageBox.Show($"{firstOrderFileName} & {secondOrderFileName} were successfully created in directory: \n\n{AppDomain.CurrentDomain.BaseDirectory + folderName}", "Success!");
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
            }
        }

        #endregion

        #region UI

        /// <summary>
        /// Sets the header on the top of the form and ensures any labels not used on startup are cleared.
        /// </summary>
        private void SetDefaultLabelTextValues()
        {
            lblHeader.Text = $"{Regex.Replace(Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title, "([A-Z])", " $1")} v{Application.ProductVersion}";
            lblSelectedFileName.Text = string.Empty;
            lblOrdersFoundCount.Text = string.Empty;
            lblErrorMessage.Text = string.Empty;
        }

        /// <summary>
        /// If the CSV is read in successfully, enable the capacity UI, otherwise, disable it.
        /// </summary>
        /// <param name="value"></param>
        private void ChangeStateOfRulesUI(bool value)
        {
            lblTruckCapacity.Enabled = value;
            lblSetReductionPercentage.Enabled = value;
            numericUpDownTruckCapacity.Enabled = value;
            numericUpDownMaximumReductionPercentage.Enabled = value;
            btnGenerateNewOrders.Enabled = value;
        }

        #endregion


    }
}
