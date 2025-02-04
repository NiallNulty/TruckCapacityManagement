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

        public MainForm()
        {
            InitializeComponent();
            SetDefaultLabelTextValues();
        }

        /// <summary>
        /// Handles logic for presing btnOpenFile
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
                return;
            }

            // If the file format is invalid, don't go any further //
            if (!IsValidFileFormat(openFileDialog.FileName))
            {
                lblErrorMessage.Text = $"Headings missing from file. Please ensure the following headings exist in order: {nameof(Order.Customer)}, {nameof(Order.OrderNumber)}, {nameof(Order.ProductCode)}, {nameof(Order.OrderQty)}";
                return;
            }

            // Create a list of orders in memory for data manipualtion later //
            CreateOrdersList(openFileDialog.FileName);

            // Let the user know what file they selected & how many orders were found in the file //
            lblSelectedFileName.Text = $"Selected File: {Path.GetFileNameWithoutExtension(openFileDialog.FileName)}";
            lblOrdersFoundCount.Text = $"{orders.Count} Order(s) Found";
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
                out _
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
    }
}
