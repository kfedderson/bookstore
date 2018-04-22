using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// Invoice ID
        /// </summary>
        int iInvoiceID = -1;

        /// <summary>
        /// search logic class
        /// </summary>
        clsSearchLogic searchLogic;

        /// <summary>
        /// variable to keep track of whether a date has previously been selected
        /// </summary>
        bool dateDropDownSelection = false;

        /// <summary>
        /// variable to keep track of whether an amount has previously been selected
        /// </summary>
        bool amountDropDownSelection = false;

        /// <summary>
        /// variable to keep track of last selected amount
        /// </summary>
        string lastSelectedAmount;

        /// <summary>
        /// variable to keep track of last selected date
        /// </summary>
        string lastSelectedDate;


        /// <summary>
        /// Public property for Invoice ID
        /// </summary>
        public int InvoiceID
        {
            get { return iInvoiceID; }
            set { iInvoiceID = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public wndSearch()
        {
            InitializeComponent();

            searchLogic = new clsSearchLogic(); //new searchLogic object
            //populate datagrid with all database rows
            dataGrid.ItemsSource = searchLogic.GetAllInvoiceData();

            //initial load of filter lists
            PopulateAllComboBoxes();
        }
        

        /// <summary>
        /// Method for when an invoice is chosen and Select button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Get invoice number from current datagrid selection ans assign to public property
                string sInvoiceID = (dataGrid.SelectedItem as clsInvoice).sID;
                Int32.TryParse(sInvoiceID, out iInvoiceID);
                this.Hide();    //return to Main
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// Method to clear filters when searching for invoices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Reset drop-down boxes to blank
                cmbSearchId.Items.Clear();
                cmbSearchAmount.Items.Clear();
                cmbSearchDate.Items.Clear();

                //display full results in DataGrid and reset filter lists
                dataGrid.ItemsSource = searchLogic.GetAllInvoiceData();
                PopulateAllComboBoxes();

                //disable Select button
                btnSelectInvoice.IsEnabled = false;

                //reset selection trackers
                dateDropDownSelection = false;
                amountDropDownSelection = false;

                //reset last selection values
                lastSelectedAmount = "";
                lastSelectedDate = "";
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// Method for when Cancel button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Close this screen and return to main menu
                this.Hide();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method for when invoice number drop-down is updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSearchId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //get sender
                ComboBox combo = (ComboBox)sender;

                //check that selection has been made, box wasn't just cleared
                if (combo.SelectedIndex != -1 && combo.Items.Count > 1)
                {
                    string selectedId = combo.SelectedItem.ToString();

                    List<clsInvoice> list = new List<clsInvoice>();
                    list = searchLogic.GetInvoiceById(selectedId);

                    //update datagrid with invoice id selected
                    dataGrid.ItemsSource = list;

                    //clear out and readd date and amount combo box values that match the invoice number
                    cmbSearchAmount.Items.Clear();
                    cmbSearchAmount.Items.Add(list[0].sAmount);
                    cmbSearchAmount.SelectedIndex = 0;
                    cmbSearchDate.Items.Clear();
                    cmbSearchDate.Items.Add(list[0].sDate);
                    cmbSearchDate.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Method for when date selection drop-down is updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSearchDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //get sender
                ComboBox combo = (ComboBox)sender;

                //check that selection has been made, box wasn't just cleared
                if (combo.SelectedIndex != -1 && combo.Items.Count > 1)
                {
                    //keep track of date selection
                    dateDropDownSelection = true;   
                    lastSelectedDate = combo.SelectedItem.ToString();
                    
                    //do if an amount has already been chosen
                    if (amountDropDownSelection)
                    {
                        //get current selected amount
                        string amount = cmbSearchAmount.SelectedItem.ToString();

                        //update datagrid with invoices that match selected date and amount
                        dataGrid.ItemsSource = searchLogic.GetInvoicesByDateAndAmount(lastSelectedDate, amount);

                        UpdateIdAmountCombos(lastSelectedDate, amount);

                        //refresh amount dropdown with only the selected value
                        cmbSearchAmount.Items.Clear();
                        cmbSearchAmount.Items.Add(lastSelectedAmount);
                        cmbSearchAmount.SelectedIndex = 0;
                    }
                    else
                    {
                        //update datagrid with invoices that match selected date
                        dataGrid.ItemsSource = searchLogic.GetAllInvoiceDataByDate(lastSelectedDate);

                        //update id and amount drop downs with only values that match the date
                        UpdateIdAmountCombos(lastSelectedDate);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method for when total amount selection drop-down is updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSearchAmount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //get sender
                ComboBox combo = (ComboBox)sender;

                //check that selection has been made, box wasn't just cleared
                if (combo.SelectedIndex != -1 && combo.Items.Count > 1)
                {
                    //keep track of amount selection
                    amountDropDownSelection = true;
                    lastSelectedAmount = combo.SelectedItem.ToString();

                    //do if an amount has already been chosen
                    if (dateDropDownSelection)
                    {
                        string date = cmbSearchDate.SelectedItem.ToString();

                        //update datagrid with invoices that match selected date and amount
                        dataGrid.ItemsSource = searchLogic.GetInvoicesByDateAndAmount(date, lastSelectedAmount);

                        //update other drop downs with only values that match amount
                        UpdateIdDateCombos(date, lastSelectedAmount);

                        //clear date dropdown and update with last selected only
                        cmbSearchDate.Items.Clear();
                        cmbSearchDate.Items.Add(lastSelectedDate);
                        cmbSearchDate.SelectedIndex = 0;
                    }
                    else
                    {
                        //update datagrid with invoice id selected
                        dataGrid.ItemsSource = searchLogic.GetAllInvoiceDataByAmount(lastSelectedAmount);

                        //update other drop downs with only values that match amount
                        UpdateIdDateCombos(lastSelectedAmount);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// Method to load the filter combo boxes
        /// </summary>
        private void PopulateAllComboBoxes()
        {
            try
            {
                //populate id list combo
                List<clsInvoice> idList = searchLogic.GetInvoiceIdList();
                foreach (clsInvoice a in idList)
                {
                    cmbSearchId.Items.Add(a.sID);
                }

                //populate date list combo
                List<clsInvoice> dateList = searchLogic.GetUniqueInvoiceDates();
                foreach (clsInvoice b in dateList)
                {
                    cmbSearchDate.Items.Add(b.sDate);
                }

                //populate amount list combo
                List<clsInvoice> amountList = searchLogic.GetUniqueInvoiceAmounts();
                foreach (clsInvoice c in amountList)
                {
                    cmbSearchAmount.Items.Add(c.sAmount);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// method to update the id and date drop downs when a filter amount is chosen
        /// </summary>
        /// <param name="amount">invoice amount</param>
        private void UpdateIdDateCombos(string amount)
        {
            try
            {
                //clear previous values
                cmbSearchId.Items.Clear();
                cmbSearchDate.Items.Clear();

                //populate date list combo
                List<clsInvoice> dateList = searchLogic.GetUniqueDatesByAmount(amount);
                foreach (clsInvoice b in dateList)
                {
                    cmbSearchDate.Items.Add(b.sDate);
                }

                //populate id list combo
                List<clsInvoice> idList = searchLogic.GetInvoiceIdListByAmount(amount);
                foreach (clsInvoice c in idList)
                {
                    cmbSearchId.Items.Add(c.sID);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// overloaded method to update the id and date drop downs when both date and amount is chosen
        /// </summary>
        /// <param name="amount">invoice amount</param>
        private void UpdateIdDateCombos(string date, string amount)
        {
            try
            {
                //clear previous values
                cmbSearchId.Items.Clear();
                cmbSearchDate.Items.Clear();

                //populate date list combo
                List<clsInvoice> dateList = searchLogic.GetUniqueDatesByAmount(amount);
                foreach (clsInvoice b in dateList)
                {
                    cmbSearchDate.Items.Add(b.sDate);
                }

                //populate id list combo
                List<clsInvoice> idList = searchLogic.GetInvoiceIdListByDateAndAmount(date, amount);
                foreach (clsInvoice c in idList)
                {
                    cmbSearchId.Items.Add(c.sID);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// method to update id and amount dropdowns when a date filter is chosen
        /// </summary>
        /// <param name="date">invoice date</param>
        private void UpdateIdAmountCombos(string date)
        {
            try
            {
                //clear previous values
                cmbSearchId.Items.Clear();
                cmbSearchAmount.Items.Clear();

                //populate id list combo
                List<clsInvoice> idList = searchLogic.GetInvoiceIdListByDate(date);
                foreach (clsInvoice b in idList)
                {
                    cmbSearchId.Items.Add(b.sID);
                }

                //populate amount list combo
                List<clsInvoice> amountList = searchLogic.GetUniqueAmountsByDate(date);
                foreach (clsInvoice c in amountList)
                {
                    cmbSearchAmount.Items.Add(c.sAmount);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// overloaded method to update the id and amount drop downs when both date and amount is chosen
        /// </summary>
        /// <param name="date">invoice date</param>
        private void UpdateIdAmountCombos(string date, string amount)
        {
            try
            {
                //clear previous values
                cmbSearchId.Items.Clear();
                cmbSearchAmount.Items.Clear();

                //populate id list combo
                List<clsInvoice> idList = searchLogic.GetInvoiceIdListByDateAndAmount(date, amount);
                foreach (clsInvoice b in idList)
                {
                    cmbSearchId.Items.Add(b.sID);
                }

                //populate amount list combo
                List<clsInvoice> amountList = searchLogic.GetUniqueAmountsByDate(date);
                foreach (clsInvoice c in amountList)
                {
                    cmbSearchAmount.Items.Add(c.sAmount);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// Method for when a selection is made within the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btnSelectInvoice.IsEnabled = true; //enable select button
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// method to handle exceptions
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }


        //End
    }
}
