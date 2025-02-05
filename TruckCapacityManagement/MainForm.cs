using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using TruckCapacityManagement.Constants;
using TruckCapacityManagement.Controllers;
using TruckCapacityManagement.Models;
using TruckCapacityManagement.Utilities;

namespace TruckCapacityManagement
{
    public partial class MainForm : Form
    {
        private List<Order> orders = new List<Order>();

        public MainForm()
        {
            InitializeComponent();
            RefreshAllLabels();
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
            openFileDialog.Filter = $"CSV files (*{AppConstants.CSVFileFormat})|*{AppConstants.CSVFileFormat}";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                orders.Clear();
                orders = DataAccess.TryReadFile(openFileDialog);
                RefreshFileAccessLabels();
                ChangeStateOfRulesUI();
            }
        }

        /// <summary>Handles logic for presing btnGenerateNewOrders.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateNewOrders_Click(object sender, EventArgs e)
        {
            RefreshRulesUI();

            // Get the data to populate the new CSV's with //
            List<List<Order>> newOrders = DataModifier.TryCreateNewOrderLists(orders, numericUpDownTruckCapacity.Value, numericUpDownMaximumReductionPercentage.Value);

            // Create the two files //
            if (newOrders.Count == 2)
            {
                FileCreation.CreateNewOrderFiles(DataAccess.GetOrderDate(), newOrders[0], newOrders[1]);
                ShowMessageBox();
            }

            RefreshAllLabels();
            UIController.SetUIEnabledValue(false);
            ChangeStateOfRulesUI();
        }

        #endregion

        #region UI

        /// <summary>
        /// Refreshes all labels that can be updated.
        /// </summary>
        private void RefreshAllLabels()
        {
            lblHeader.Text = UIController.GetHeader();
            lblSelectedFileName.Text = UIController.GetSelectedFileName();
            lblOrdersFoundCount.Text = UIController.GetOrdersFoundCountText();
            lblErrorMessage.Text = UIController.GetErrorMessage();
        }

        /// <summary>
        /// Refreshes the labels in relation to File Access.
        /// </summary>
        private void RefreshFileAccessLabels()
        {
            lblSelectedFileName.Text = UIController.GetSelectedFileName();
            lblOrdersFoundCount.Text = UIController.GetOrdersFoundCountText();
            lblErrorMessage.Text = UIController.GetErrorMessage();
        }

        /// <summary>
        /// Refreshes the labels in relation to Rules.
        /// </summary>
        private void RefreshRulesUI()
        {
            lblErrorMessage.Text = UIController.GetErrorMessage();
        }

        /// <summary>
        /// If the CSV is read in successfully, enable the capacity UI, otherwise, disable it.
        /// </summary>
        private void ChangeStateOfRulesUI()
        {
            bool value = UIController.GetUIEnabledValue();
            lblTruckCapacity.Enabled = value;
            lblSetReductionPercentage.Enabled = value;
            numericUpDownTruckCapacity.Enabled = value;
            numericUpDownTruckCapacity.Value = 0;
            numericUpDownMaximumReductionPercentage.Enabled = value;
            numericUpDownMaximumReductionPercentage.Value = 0;
            btnGenerateNewOrders.Enabled = value;
        }

        /// <summary>
        /// Displays the result of order file creation.
        /// </summary>
        private void ShowMessageBox()
        {
            List<string> messageBoxText = UIController.GetMessageBoxText();

            if (messageBoxText.Count == 2)
            {
                MessageBox.Show(messageBoxText[0], messageBoxText[1]);
            }
        }

        #endregion
    }
}
