using BookStore.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region properties
        /// <summary>
        /// Has the info for the invoice. invNum is -1 if
        /// the invoice isn't in the database.
        /// </summary>
        InvoiceCls MyInvoice;
        /// <summary>
        /// The window object for finding an invoice to 
        /// work with.
        /// </summary>
        wndSearch winSearch;
        /// <summary>
        /// The window object for adding items to be
        /// possible to buy.
        /// </summary>
        AddItems winUpdate;

        /// <summary>
        /// Is true when an invoice is being altered. 
        /// When true, the Update window isn't allowed to be used.
        /// </summary>
        bool editing = false;

        #endregion

        /// <summary>
        /// Constructor. Instantiates window objects and
        /// maximizes self.
        /// </summary>
        public MainWindow()
        {
            winSearch = new wndSearch();
            winUpdate = new AddItems();
            //At startup, shouldn't have an invoice selected.
            try
            {
                MyInvoice = new InvoiceCls(-1);

                InitializeComponent();
                WindowState = WindowState.Maximized;
                LoadInvoice();
            }
            catch (Exception ex)
            {
                HandleError("MainWindow.xaml.cs", "MainWindow", ex.Message);
            }
            //SetUpComboBoxes();
            //Is there a current Invoice ID?
            //OpenCurrentInvoice();
            //sets the combo boxes to the correct selections
            //sets the data grid and whatever else. Label.
            //Else, Delete isn't an option
        }

        #region Methods
        /// <summary>
        /// Closes the program because the MainWindow was closed.
        /// Have to force it closed because of ShowDialog() or something.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        #region Menu Items (Search Window, Update Window)
        /// <summary>
        /// Opens the Search Invoices window. (And hides main window.)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MI_Search_Click(object sender, RoutedEventArgs e)
        {
            if (!editing)
            {
                try
                {
                    this.Hide();
                    winSearch.ShowDialog();
                    //check to see if an invoice was selected. (Negative number means 
                    //cancel was clicked on the winSearch
                    if (winSearch.InvoiceID >= 0)
                    {
                        btnAddInvoice.IsEnabled = true;
                        btnEditInvoice.IsEnabled = true;
                        btnDeleteInvoice.IsEnabled = true;

                        MyInvoice.invNum = winSearch.InvoiceID;
                        MyInvoice.LoadFromDatabase();
                        dpDatePick.SelectedDate = MyInvoice.date;
                        //dpDatePick.Text = MyInvoice.date.ToString();
                        lblTotalNum.Content = MyInvoice.totalCost.ToString();
                        lblIDnum.Content = MyInvoice.invNum;
                        dgItems.ItemsSource = MyInvoice.itemList;
                        dgItems.Items.Refresh();
                    }
                    this.Show();
                }
                catch (Exception ex)
                {
                    HandleError("MainWindow.xaml.cs", "MI_Search_Click", ex.Message);
                }
            }
            else
            {
                //Show a label that explains "Save you work before going to Updating."
                MessageBox.Show("You must Save or Cancel before leaving this page.", "Something Happened", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //MessageBox.Show()
            }
        }

        /// <summary>
        /// Opens the Update Items window. (And hides main window.)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MI_Update_Click(object sender, RoutedEventArgs e)
        {
            if (!editing)
            {
                try
                {
                    winUpdate = new AddItems();
                    this.Hide();
                    winUpdate.ShowDialog();
                    //In case an item was added or deleted, refresh the item list.
                    MyInvoice.FillAllItems();

                    MyInvoice.LoadFromDatabase();

                    LoadInvoice();

                    dgItems.Items.Refresh();
                    //Is there a selected invoice at this point? if so, refresh it
                    this.Show();
                }
                catch(Exception ex)
                {
                    HandleError("MainWindow.xaml.cs", "MI_Update_Click", ex.Message);
                }
            }
            else
            {
                //Show a label that explains "Save you work before going to Updating."
                MessageBox.Show("You must Save or Cancel before leaving this page.", "Something Happened", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //MessageBox.Show()
            }
        }
        #endregion

        #region Right Top 3 Buttons. (Add, Edit, Delete)
        /// <summary>
        /// Adds an invoice and blanks out everything.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddInvoice_Click(object sender, RoutedEventArgs e)
        {
            editing = true;
            btnAddInvoice.IsEnabled = false;
            btnEditInvoice.IsEnabled = false;
            btnDeleteInvoice.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;

            lblIDnum.Content = "TBD";

            dpDatePick.IsEnabled = true;
            cbItems.IsEnabled = true;
            btnAddToCart.IsEnabled = true;

            try
            {
                MyInvoice = new InvoiceCls(-1);
                MyInvoice.GetNewInvoice();
                //lblIDnum.Content = MyInvoice.invNum;
                lblTotalNum.Content = "";
                dgItems.ItemsSource = MyInvoice.itemList;
                dpDatePick.SelectedDate = MyInvoice.date;
                //dpDatePick.Text = MyInvoice.date.ToString();
            }
            catch (Exception ex)
            {
                HandleError("MainWindow.xaml.cs", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Enables the invoice UI stuff so they can be changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            editing = true;
            btnAddInvoice.IsEnabled = false;
            btnEditInvoice.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;

            dpDatePick.IsEnabled = true;
            cbItems.IsEnabled = true;
            btnAddToCart.IsEnabled = true;

            try
            {
                dgItems.Items.Refresh();

                int temp = 0;
                foreach (Item q in MyInvoice.itemList)
                {
                    temp++;
                }

                if (temp != 0)
                {
                    btnRemoveFromCart.IsEnabled = true;
                }
                else
                {
                    btnRemoveFromCart.IsEnabled = false;
                    MyInvoice.totalCost = 0.00M;
                }
            }
            catch (Exception ex)
            {
                HandleError("MainWindow.xaml.cs", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Deletes the invoice and disables the invoice editing stuff.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyInvoice.DeleteFromDatabase();
                //Clear out InvoiceObj. How to avoid a memory leak? Does it auto garbage collect?
                editing = false;

                btnAddInvoice.IsEnabled = true;
                btnEditInvoice.IsEnabled = false;
                btnDeleteInvoice.IsEnabled = false;

                lblIDnum.Content = "TBD";

                dpDatePick.Text = "";
                dpDatePick.IsEnabled = false;

                txtBItemPrice.Text = "";
                cbItems.SelectedIndex = -1;
                cbItems.IsEnabled = false;

                btnAddToCart.IsEnabled = false;
                btnRemoveFromCart.IsEnabled = false;

                btnCancel.IsEnabled = false;
                btnSave.IsEnabled = false;

                //blank out invoice.
                MyInvoice.itemList.Clear();

                dgItems.Items.Refresh();

                lblTotalNum.Content = "";
            }
            catch (Exception ex)
            {
                HandleError("MainWindow.xaml.cs", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Right Middle (Add to cart, cbItems.Selection Changed)
        /// <summary>
        /// Adds item to the invoice object's list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //add the selected item object to the InvoiceObj.ItemList
                if (cbItems.SelectedIndex >= 0)
                {
                    MyInvoice.AddItemToCart(cbItems.SelectedIndex);
                    dgItems.Items.Refresh();
                    lblTotalNum.Content = MyInvoice.totalCost;
                    btnRemoveFromCart.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                HandleError("MainWindow.xaml.cs", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// The selected date was changed, so update the invoice object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpDatePick_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //Put into the invoice object
            try
            {
                if (dpDatePick.SelectedDate.HasValue)
                {
                    MyInvoice.date = DateTime.Parse(dpDatePick.SelectedDate.ToString());
                }
            }
            catch (Exception ex)
            {
                HandleError("MainWindow.xaml.cs", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Since the selected item changed, change the price label to match.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //change the txtBItemPrice to show the selected item's price.
                if (cbItems.SelectedIndex > -1)
                {
                    txtBItemPrice.Text = MyInvoice.allItems[cbItems.SelectedIndex].sPrice;
                }
                else
                {
                    txtBItemPrice.Text = "";
                }
            }
            catch (Exception ex)
            {
                HandleError("MainWindow.xaml.cs", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Right Bottom 2 Buttons (Cancel, Save)
        /// <summary>
        /// Commits the invoice object's info to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int temp = 0;
                foreach (Item i in MyInvoice.itemList)
                {
                    temp++;
                }
                if (temp > 0)
                {
                    //save to database and can edit
                    MyInvoice.SaveToDatabase();
                    btnEditInvoice.IsEnabled = true;
                }
                else
                {
                    btnDeleteInvoice.IsEnabled = false;
                    dpDatePick.SelectedDate = DateTime.Today;
                    MessageBox.Show("There weren't any items added to the invoice, so nothing was saved.", "Nothing Happened", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                //otherwise it acts only like a Cancel.
                editing = false;

                btnRemoveFromCart.IsEnabled = false;

                btnAddInvoice.IsEnabled = true;

                btnCancel.IsEnabled = false;
                btnSave.IsEnabled = false;

                dpDatePick.IsEnabled = false;
                txtBItemPrice.Text = "";
                cbItems.SelectedIndex = -1;
                cbItems.IsEnabled = false;
                btnAddToCart.IsEnabled = false;

                lblIDnum.Content = MyInvoice.invNum;

                if (MyInvoice.invNum == -1)
                    lblIDnum.Content = "TBD";
            }
            catch (Exception ex)
            {
                HandleError("MainWindow.xaml.cs", "btnSave_Click", ex.Message);
            }
        }

        /// <summary>
        /// Cancels the edits or adding of the invoice. Invoice is loaded
        /// from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //reload invoice from database
                MyInvoice.LoadFromDatabase();

                editing = false;
                if (MyInvoice.invNum > -1)
                {
                    btnEditInvoice.IsEnabled = true;
                    btnDeleteInvoice.IsEnabled = true;
                }
                else
                {
                    btnEditInvoice.IsEnabled = false;
                    btnDeleteInvoice.IsEnabled = false;
                    MyInvoice.itemList.Clear();
                    dgItems.Items.Refresh();
                }

                btnAddInvoice.IsEnabled = true; //Is this ever /not/ true? Yes. When an invoice
                                                //isn't saved yet or cancelled.

                dgItems.Items.Refresh();
                lblTotalNum.Content = MyInvoice.totalCost.ToString();

                dpDatePick.SelectedDate = MyInvoice.date;
                //dpDatePick.Text = MyInvoice.date.ToString();
                dpDatePick.IsEnabled = false;

                txtBItemPrice.Text = "";

                btnRemoveFromCart.IsEnabled = false;

                cbItems.SelectedIndex = -1;
                cbItems.IsEnabled = false;

                btnAddToCart.IsEnabled = false;

                btnCancel.IsEnabled = false;
                btnSave.IsEnabled = false;
            }
            catch (Exception ex)
            {
                HandleError("MainWindow.xaml.cs", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Left Functions (Remove from cart)
        /// <summary>
        /// Removes item from invoice object's list and updates totalCost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgItems.SelectedIndex >= 0)
                {
                    decimal subtract = MyInvoice.itemList[dgItems.SelectedIndex].price;
                    MyInvoice.itemList.Remove(MyInvoice.itemList[dgItems.SelectedIndex]);
                    MyInvoice.totalCost -= subtract;
                    dgItems.SelectedIndex = -1;
                }

                //Check to see if there's more items to remove
                int temp = 0;
                foreach (Item q in MyInvoice.itemList)
                {
                    temp++;
                }

                if (temp == 0)
                {
                    btnRemoveFromCart.IsEnabled = false;
                    MyInvoice.totalCost = 0.00M; //double numbers are being weird so
                                                 //sometimes it gets off by a cent or two.
                }

                dgItems.Items.Refresh();
                lblTotalNum.Content = MyInvoice.totalCost;
            }
            catch (Exception ex)
            {
                HandleError("MainWindow.xaml.cs", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Supplementary Methods (Load, HandleError)
        /// <summary>
        /// Gets the invoice object's info and puts it into the UI.
        /// </summary>
        private void LoadInvoice()
        {
            //Creates/fills the Invoice stuff.
            //Datagrid
            //date
            //combo box of items
            //total price
            //ID
            try
            {
                if (MyInvoice.invNum > -1)
                {
                    lblIDnum.Content = MyInvoice.invNum;
                    dgItems.ItemsSource = MyInvoice.itemList;
                    dpDatePick.SelectedDate = MyInvoice.date;
                    //dpDatePick.Text = MyInvoice.date.ToString();

                    lblTotalNum.Content = MyInvoice.totalCost;

                }
                else
                {
                    //blank everything out.
                }

                cbItems.Items.Clear();

                foreach (Item i in MyInvoice.allItems)
                {
                    cbItems.Items.Add(i);
                    //dgItems.ItemsSource = MyInvoice.allItems;
                }
            }
            catch (Exception ex)
            {
                HandleError("MainWindow.xaml.cs", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method to handle exceptions
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show("Sorry, something went wrong.\n" + sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
        #endregion

        #endregion //Methods as a whole
    }
}
