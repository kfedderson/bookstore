using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;

namespace BookStore.Items
{
    class clsItemLogic
    {

        #region Attributes

        clsDataAccess dataAccess;
        DataSet itemSet;
        int numOfElements;
        string tableName;
        int invoiceInquiryLength;
        ObservableCollection<string> itemCodes;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public clsItemLogic()
        {
            itemCodes = new ObservableCollection<string>();
            invoiceInquiryLength = 0;
            tableName = "Products";
            dataAccess = new clsDataAccess();
            getNewData();
            for (int i = 0; i < numOfElements; i++)
            {
                itemCodes.Add(Convert.ToString(itemSet.Tables[0].Rows[i][0]));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes dataset for database info
        /// </summary>
        /// <returns></returns>
        public DataSet getNewData()
        {
            itemSet = dataAccess.ExecuteSQLStatement("SELECT * FROM [" + tableName + "]", ref numOfElements);
            return itemSet;
        }

        /// <summary>
        /// Handling for updating items in database
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="Title"></param>
        /// <param name="Author"></param>
        /// <param name="Price"></param>
        public void setItem(string itemCode, string Title, string Author, double Price)
        {
            if (!(Title.Length > 0) || !(Author.Length > 0) || (Price <= 0))
            {
                throw new Exception("Incorrect. Please recheck input values");
            }
            dataAccess.ExecuteScalarSQL("UPDATE [" + tableName + "] SET Title = '" + Title +
                "', Author = '" + Author + "', Price =" + Price + " WHERE ItemCode = '" + itemCode + "'");
            getNewData();
        }

        public int getSize()
        {
            return numOfElements;
        }


        /// <summary>
        /// Adding a new item
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="Title"></param>
        /// <param name="Author"></param>
        /// <param name="Price"></param>
        internal void addItem(string itemCode, string Title, string Author, double Price)
        {
            if ((itemCode.Length == 0) || (Title.Length == 0) || (Author.Length == 0) || (Price <= 0))
            {
                throw new Exception("Incorrect format. Please recheck values");
            }
            dataAccess.ExecuteScalarSQL("INSERT INTO [" + tableName + "] (ItemCode, Title, Author, Price)" +
                                        " VALUES ('" + itemCode + "','" + Title + "','" + Author + "'," + Price + ")");
            itemCodes.Add(itemCode);
            getNewData();

        }


        /// <summary>
        /// Deleting an item
        /// </summary>
        /// <param name="itemCode"></param>
        public void deleteItem(string itemCode)
        {
            DataSet invoices = dataAccess.ExecuteSQLStatement("SELECT * FROM InvoiceItems WHERE itemCode = '" + itemCode + "'", ref invoiceInquiryLength);
            if (invoiceInquiryLength == 0)
            {
                dataAccess.ExecuteScalarSQL("DELETE FROM [" + tableName + "] WHERE itemCode = '" + itemCode + "'");
                itemCodes.Remove(itemCode);
                getNewData();
            }
            else
            {
                String usedList = "";

                for (int i = 0; i < invoiceInquiryLength; i++)
                {
                    usedList += (invoices.Tables[0].Rows[i][0] + ", ");
                }

                throw new Exception("Unable to delete. Item is in invoice: " + usedList );
            }
        }

        /// <summary>
        /// Handling for checkin itemCod duplicates
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public bool checkIfItemExists(string itemCode)
        {
            return itemCodes.Contains(itemCode);
        }


        #endregion



    }
}
