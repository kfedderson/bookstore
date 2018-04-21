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

        /// <summary>
        /// Constructor. Instantiates window objects and
        /// maximizes self.
        /// </summary>
        public MainWindow()
        {
            winSearch = new wndSearch();
            winUpdate = new AddItems();
            //At startup, shouldn't have an invoice selected.
            MyInvoice = new InvoiceCls(-1);
            
            InitializeComponent();
            //WindowState = WindowState.Maximized;
            LoadInvoice();

            //SetUpComboBoxes();
            //Is there a current Invoice ID?
            //OpenCurrentInvoice();
            //sets the combo boxes to the correct selections
            //sets the data grid and whatever else. Label.
            //Else, Delete isn't an option
        }


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

        /// <summary>
        /// Opens the Search Invoices window. (And hides main window.)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MI_Search_Click(object sender, RoutedEventArgs e)
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
                //LoadInvoice(winSearch.InvoiceID);
            }
            this.Show();
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
                winUpdate = new AddItems();
                this.Hide();
                winUpdate.ShowDialog();
                //In case an item was added or deleted, refresh the item list.
                MyInvoice.FillAllItems();
                //Is there a selected invoice at this point? if so, refresh it
                this.Show();
            }
            else
            {
                //Show a label that explains "Save you work before going to Updating."
            }
        }
        

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //save to database
            MyInvoice.SaveToDatabase();

            editing = false;
            btnEditInvoice.IsEnabled = true;
            btnAddInvoice.IsEnabled = true;

            btnCancel.IsEnabled = false;
            btnSave.IsEnabled = false;

            dpDatePick.IsEnabled = false;
            cbItems.IsEnabled = false;
            btnAddToCart.IsEnabled = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //reload invoice from database
            MyInvoice.LoadFromDatabase();

            editing = false;
            btnEditInvoice.IsEnabled = true;
            btnAddInvoice.IsEnabled = true;

            btnCancel.IsEnabled = false;
            btnSave.IsEnabled = false;

            dpDatePick.IsEnabled = false;
            cbItems.IsEnabled = false;
            btnAddToCart.IsEnabled = false;
        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            //add the selected item object to the InvoiceObj.ItemList
        }

        private void dpDatePick_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //Put into the invoice object
        }

        private void btnAddInvoice_Click(object sender, RoutedEventArgs e)
        {
            editing = true;
            btnAddInvoice.IsEnabled = false;
            btnEditInvoice.IsEnabled = false;
            btnDeleteInvoice.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;

            dpDatePick.IsEnabled = true;
            cbItems.IsEnabled = true;
            btnAddToCart.IsEnabled = true;

            MyInvoice = new InvoiceCls(-1);
            MyInvoice.GetNewInvoiceID();
            lblIDnum.Content = MyInvoice.invNum;
        }

        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            editing = true;
            btnAddInvoice.IsEnabled = false;
            btnEditInvoice.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;

        }

        /// <summary>
        /// Deletes the invoice and disables the invoice editing stuff.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            MyInvoice.DeleteFromDatabase();
            //Clear out InvoiceObj. How to avoid a memory leak? Does it auto garbage collect?
            editing = false;

            btnDeleteInvoice.IsEnabled = false;
            lblIDnum.Content = "TBD";
            btnAddInvoice.IsEnabled = true;
            dpDatePick.Text = "";
            dpDatePick.IsEnabled = false;
            cbItems.IsEnabled = false;
            btnAddToCart.IsEnabled = false;
            btnCancel.IsEnabled = false;
            btnSave.IsEnabled = false;

            //blank out invoice.
            MyInvoice = null;
        }

        private void LoadInvoice()
        {
            //Creates/fills the Invoice stuff.
            //Datagrid
            //date
            //combo box of items
            //total price
            //ID
            if (MyInvoice.invNum > -1)
            {
                lblID.Content = MyInvoice.invNum;
                dgItems.ItemsSource = MyInvoice.itemList;
                dpDatePick.Text = MyInvoice.date.ToString();
                cbItems.ItemsSource = MyInvoice.allItems;
                lblTotal.Content = "$" + MyInvoice.totalCost;
            }
            else
            {
                //blank everything out.
            }
            
            foreach (Item i in MyInvoice.allItems)
            {
                cbItems.Items.Add(i);
            }
        }

        private void cbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //change the txtBItemPrice to show the selected item's price.
            if(cbItems.SelectedIndex > -1)
            {
                txtBItemPrice.Text = "$" + MyInvoice.allItems[cbItems.SelectedIndex].price.ToString();
            }
            else
            {
                txtBItemPrice.Text = "";
            }
        }
    }
}
