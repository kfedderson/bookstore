using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    /// <summary>
    /// Stores the current invoice's info and handles
    /// the SQL statements for the Main Window.
    /// </summary>
    class InvoiceCls
    {
        #region Properties
        /// <summary>
        /// The database class object
        /// </summary>
        clsDataAccess data;

        /// <summary>
        /// The invoice's ID
        /// </summary>
        public int invNum;
        /// <summary>
        /// The date of the invoice
        /// </summary>
        public DateTime date;
        /// <summary>
        /// List of the items on this invoice
        /// </summary>
        public List<Item> itemList;
        /// <summary>
        /// Total price of all items on the list
        /// </summary>
        public decimal totalCost;

        /// <summary>
        /// Contains all items so that the comboBox can pass it's 
        /// index and this class uses this list to add an item to 
        /// the invoice's list.
        /// </summary>
        public List<Item> allItems;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="inv"></param>
        public InvoiceCls(int inv)
        {
            data = new clsDataAccess();
            allItems = new List<Item>();
            itemList = new List<Item>();
            try
            {
                allItems.Clear();
                FillAllItems();
                invNum = inv;
                LoadFromDatabase();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Fills the list AllItems, with every buyable item.
        /// </summary>
        public void FillAllItems()
        {
            try
            {
                int num = 0;
                DataSet ds = data.ExecuteSQLStatement("SELECT ItemCode," +
                    " Title, Price FROM Products", ref num);
                allItems.Clear();

                for (int i = 0; i < num; i++)
                {
                    Item temp = new Item(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString());
                    allItems.Add(temp);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Loads the object from the database.
        /// </summary>
        public void LoadFromDatabase()
        {
            //Cancel was clicked
            //Resets the object from the database.
            try
            {

                if (invNum > -1)
                {
                    itemList.Clear();

                    //Get the Invoice info. 
                    int num = 0;
                    DataSet ds = data.ExecuteSQLStatement("SELECT InvoiceDate, TotalAmount FROM Invoices WHERE" +
                        " InvoiceNumber = " + invNum.ToString(), ref num);
                    date = DateTime.Parse(ds.Tables[0].Rows[0][0].ToString());
                    totalCost = Decimal.Parse(ds.Tables[0].Rows[0][1].ToString());

                    //Get the item info
                    ds = data.ExecuteSQLStatement("SELECT ItemCode FROM LineItems WHERE InvoiceNumber = " +
                        invNum, ref num);

                    for (int n = 0; n < num; n++)
                    {
                        string code = ds.Tables[0].Rows[n][0].ToString();
                        Item temp = new Item(code, data.ExecuteScalarSQL("SELECT Title FROM Products WHERE ItemCode = '" + code + "'").ToString(), data.ExecuteScalarSQL("SELECT Price FROM Products WHERE ItemCode = '" + code + "'").ToString());
                        itemList.Add(temp);
                    }
                }
                else
                {
                    itemList.Clear();
                    totalCost = 0;
                    date = DateTime.Today;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Commits invoice object to database.
        /// </summary>
        public void SaveToDatabase()
        {
            //Save was clicked
            //Update database with the info from this.
            //Delete all info about this ID. (Might not exist yet)
            //Then Insert all this info. 
            try
            {
                if (invNum >= 0)
                {
                    DeleteFromDatabase();
                }

                data.ExecuteNonQuery("INSERT INTO Invoices(InvoiceDate, TotalAmount) VALUES('" +
                    date + "', " + totalCost + ")");

                string highest = data.ExecuteScalarSQL("SELECT TOP 1 InvoiceNumber FROM" +
                    " Invoices ORDER BY InvoiceNumber DESC");

                //add one so it's a new ID
                highest = Int32.Parse(highest).ToString();
                //Console.WriteLine(highest + " is highest");

                invNum = Int32.Parse(highest);

                int i = 1;
                foreach (Item it in itemList)
                {
                    data.ExecuteNonQuery("INSERT INTO LineItems(InvoiceNumber, LineItemNumber, ItemCode) VALUES(" +
                        invNum + ", " + i + ", '" + it.itemCode + "')");
                    i++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deletes this invoice from the database.
        /// </summary>
        public void DeleteFromDatabase()
        {
            try
            {
                data.ExecuteNonQuery("DELETE * FROM LineItems WHERE InvoiceNumber =" + invNum);
                data.ExecuteNonQuery("DELETE * FROM Invoices WHERE InvoiceNumber =" + invNum);
                invNum = -1;
                itemList.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// An item is added to the cart and the total
        /// price is adjusted.
        /// </summary>
        /// <param name="index"></param>
        public void AddItemToCart(int index)
        {
            try
            {
                if (index >= 0)
                {
                    itemList.Add(allItems[index]);

                    totalCost = 0.00M;

                    foreach (Item i in itemList)
                    {
                        totalCost += i.price;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sets up a new invoice.
        /// </summary>
        public void GetNewInvoice()
        {
            try
            {
                invNum = -1; //not in the database.

                itemList.Clear();

                date = DateTime.Today;

                totalCost = 0.00M;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateInvoicePrices()
        {
            totalCost = 0.00M;

            foreach (Item i in itemList)
            {
                totalCost += i.price;
            }
        }
    }
}
