using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TruckCapacityManagement.Controllers
{
    public class UIController
    {
        private static string Header = string.Empty;
        private static string SelectedFileName = string.Empty;
        private static string OrdersFoundCountText = string.Empty;
        private static string ErrorMessage = string.Empty;
        private static bool UIEnabledValue = false;
        private static List<string> MessageBoxText = new List<string>();

        /// <summary>
        /// Gets the version number of the project and also adds spacing to the project title
        /// to add to the top of the form.
        /// </summary>
        /// <returns></returns>
        public static string GetHeader()
        {
            return $"{Regex.Replace(Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title, "([A-Z])", " $1")} v{Application.ProductVersion}";
        }

        /// <summary>
        /// Sets the Selected File Name the user will see.
        /// </summary>
        /// <param name="selectedFileName"></param>
        public static void SetSelectedFileName(string selectedFileName)
        {
            SelectedFileName = selectedFileName;
        }

        /// <summary>
        /// Gets the selected File Name the user will see.
        /// </summary>
        /// <returns></returns>
        public static string GetSelectedFileName()
        {
            string selectedFileName = SelectedFileName;
            SelectedFileName = string.Empty;
            return selectedFileName;
        }

        /// <summary>
        /// Sets the Orders Found count the user will see.
        /// </summary>
        /// <param name="ordersFoundCountText"></param>
        public static void SetOrdersFoundCountText(string ordersFoundCountText)
        {
            OrdersFoundCountText = ordersFoundCountText;
        }

        /// <summary>
        /// Gets the Orders Found count the user will see.
        /// </summary>
        /// <returns></returns>
        public static string GetOrdersFoundCountText()
        {
            string ordersFoundCountText = OrdersFoundCountText;
            OrdersFoundCountText = string.Empty;
            return ordersFoundCountText;
        }

        /// <summary>
        /// Sets the error message the user will see.
        /// </summary>
        /// <param name="errorMessage"></param>
        public static void SetErrorMessage(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Gets the error message the user will see.
        /// </summary>
        /// <returns></returns>
        public static string GetErrorMessage() 
        {
            string errorMessage = ErrorMessage;
            ErrorMessage = string.Empty;
            return errorMessage;
        }

        /// <summary>
        /// Sets the UI enabled value. This can prevent the user from progressing in the form if the file is the incorrect format.
        /// </summary>
        /// <param name="value"></param>
        public static void SetUIEnabledValue(bool value)
        {
            UIEnabledValue = value;
        }

        /// <summary>
        /// Gets the UI Enabled value.
        /// </summary>
        /// <returns></returns>
        public static bool GetUIEnabledValue() 
        {
            return UIEnabledValue;
        }

        /// <summary>
        /// Sets the values for the message box message and title.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public static void SetMessageBoxText(string message, string title)
        {
            MessageBoxText.Clear();

            MessageBoxText.Add(message);
            MessageBoxText.Add(title);
        }

        /// <summary>
        /// Gets the values to display for the message box tile and message.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetMessageBoxText()
        {
            List<string> messageBoxText = new List<string>();

            try
            {
                messageBoxText.Add(MessageBoxText[0]);
                messageBoxText.Add(MessageBoxText[1]);
                MessageBoxText.Clear();
            }
            catch (Exception ex)
            {
                messageBoxText.Add("Error");
                messageBoxText.Add(ex.Message);
                MessageBoxText.Clear();
            }

            return messageBoxText;
        }
    }
}
