using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Data;

namespace BookStore.Items
{
    /// <summary>
    /// Interaction logic for AddItems.xaml
    /// </summary>
    public partial class AddItems : Window
    {

        #region Attributes

        clsItemLogic itemLogic;
        bool addMode;

        #endregion

        #region Constructor
        public AddItems()
        {
            addMode = true;
            itemLogic = new clsItemLogic();
            InitializeComponent();
            fillDataGrid();
            clear();
            WindowState = WindowState.Maximized;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clears Selection of Items
        /// </summary>
        private void clear()
        {
            txtbxItemCode.Text = "";
            txtbxBookName.Text = "";
            txtbxAuthor.Text = "";
            txtbxPrice.Text = "";
            txtbxItemCode.IsEnabled = true;
            btnAddNewItem.IsEnabled = true;
            btnEditItem.IsEnabled = false;
            btnDeleteItem.IsEnabled = false;
            addMode = true;
        }

        /// <summary>
        /// Fills DataGrid with information from database
        /// </summary>
        private void fillDataGrid()
        {
            itemDataGrid.ItemsSource = new DataView(itemLogic.getNewData().Tables[0]);
        }

        /// <summary>
        /// Event for selectionChanged event of data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Selects a book
            try
            {
                addMode = false;
                txtbxItemCode.IsEnabled = false;
                btnEditItem.IsEnabled = true;
                btnAddNewItem.IsEnabled = false;
                btnDeleteItem.IsEnabled = true;
                fillTextBoxes();
            }
            catch (Exception)
            {
                clear();
            }

        }

        /// <summary>
        /// Button to Delete item off list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            //Displays an error message
            fillTextBoxes();
            try
            {
                if (!addMode)
                {
                    itemLogic.deleteItem(txtbxItemCode.Text);
                    fillDataGrid();
                    clear();
                }
                else
                {
                    throw new Exception("Please select an item to delete");
                }
            }

            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        /// <summary>
        /// Button to Edit an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {
            //will open another window to edit selected item's description and price
            try
            {
                if (!addMode)
                {
                    itemLogic.setItem(txtbxItemCode.Text, txtbxBookName.Text, txtbxAuthor.Text, Double.Parse(txtbxPrice.Text));
                    fillDataGrid();
                    clear();

                }
                else
                {
                    throw new Exception("Editing is not enabled,");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("The information you entered was incorrect");
            }
            catch(Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        /// <summary>
        /// Button to add a new item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNewItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (addMode)
                {
                   if (!itemLogic.checkIfItemExists(txtbxItemCode.Text))
                    {
                        itemLogic.addItem(txtbxItemCode.Text, txtbxBookName.Text, txtbxAuthor.Text, Double.Parse(txtbxPrice.Text));
                        fillDataGrid();
                        clear();
                    }
                    else
                    {
                        throw new Exception("Item Code exists.");
                    }
                }
                else
                {
                    throw new Exception("Adding an item is currently not alloweed.");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Information that was entered is incorrect");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }


        /// <summary>
        /// Fills text books with data from selection from database
        /// </summary>
        private void fillTextBoxes()
        {
            DataRowView dataRow = (DataRowView)itemDataGrid.SelectedItem;
            txtbxItemCode.Text = dataRow.Row.ItemArray[0].ToString();
            txtbxBookName.Text = dataRow.Row.ItemArray[1].ToString();
            txtbxAuthor.Text = dataRow.Row.ItemArray[2].ToString();
            txtbxPrice.Text = dataRow.Row.ItemArray[3].ToString();

        }


        /// <summary>
        /// Button to clear data in editing textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearSelection_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }


        #endregion
    }

}


