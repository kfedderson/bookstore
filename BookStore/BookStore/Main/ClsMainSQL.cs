using BookStore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Main
{
    /// <summary>
    /// Has all the SQL stuff for the Main
    /// Window.
    /// </summary>
    class ClsMainSQL
    {
        /// <summary>
        /// Database object
        /// </summary>
        clsDataAccess data;

        /// <summary>
        /// Stores items and such.
        /// </summary>
        DataSet ds;
        /// <summary>
        /// How many things are in ds.
        /// </summary>
        int num;

        /// <summary>
        /// Constructor
        /// </summary>
        public ClsMainSQL()
        {
            data = new clsDataAccess();
        }

        /// <summary>
        /// Updates an existing invoice's stuff on the Invoice table
        /// </summary>
        /// <param name="i"></param>
        public void SaveExisting(InvoiceCls i)
        {
            //data.ExecuteNonQuery("DELETE * FROM Invoices WHERE InvoiceNumber =" + invNum);
            data.ExecuteNonQuery("UPDATE Invoices SET InvoiceDate = '" + i.date + "', TotalAmount = " +
                i.totalCost + " WHERE InvoiceNumber = " + i.invNum);

            data.ExecuteNonQuery("DELETE * FROM LineItems WHERE InvoiceNumber = " + i.invNum);
        }

        /// <summary>
        /// Creates a new invoice in Invoices table.
        /// </summary>
        /// <param name="i"></param>
        public void CreateNew(ref int invNum, InvoiceCls i)
        {
            //else, insert the Invoice info and then find the highest number
            data.ExecuteNonQuery("INSERT INTO Invoices(InvoiceDate, TotalAmount) VALUES('" +
                i.date + "', " + i.totalCost + ")");

            string highest = data.ExecuteScalarSQL("SELECT TOP 1 InvoiceNumber FROM" +
                " Invoices ORDER BY InvoiceNumber DESC");

            //And set invNum to it
            highest = Int32.Parse(highest).ToString();
            //Console.WriteLine(highest + " is highest");

            i.invNum = Int32.Parse(highest);
        }

        /// <summary>
        /// Inserts current item into the LineItems table.
        /// </summary>
        /// <param name="invNum"></param>
        /// <param name="i"></param>
        /// <param name="it"></param>
        public void InsertItemLine(int invNum, int i, Item it)
        {
            data.ExecuteNonQuery("INSERT INTO LineItems(InvoiceNumber, LineItemNumber, ItemCode) VALUES(" +
                        invNum + ", " + i + ", '" + it.itemCode + "')");
        }

        /// <summary>
        /// Deletes the invoice based on its ID#
        /// </summary>
        /// <param name="invNum"></param>
        public void DeleteInvoice(int invNum)
        {
            data.ExecuteNonQuery("DELETE * FROM LineItems WHERE InvoiceNumber =" + invNum);
            data.ExecuteNonQuery("DELETE * FROM Invoices WHERE InvoiceNumber =" + invNum);
        }

        /// <summary>
        /// Returns the date for the Invoice.
        /// </summary>
        /// <param name="invNum"></param>
        /// <returns></returns>
        public DateTime GetDate(int invNum)
        {
            DateTime dt;
            dt = DateTime.Parse(data.ExecuteScalarSQL("SELECT InvoiceDate FROM Invoices WHERE" +
                        " InvoiceNumber = " + invNum.ToString()));

            return dt;
        }

        /// <summary>
        /// Returns the totalcost for the Invoice.
        /// </summary>
        /// <param name="invNum"></param>
        /// <returns></returns>
        public Decimal GetTotalCost(int invNum)
        {
            decimal d;
            d = Decimal.Parse(data.ExecuteScalarSQL("SELECT TotalAmount FROM Invoices WHERE" +
                        " InvoiceNumber = " + invNum.ToString()));
            return d;
        }

        /// <summary>
        /// Fills ds and sets num.
        /// </summary>
        /// <param name="invNum"></param>
        public void SetUpItemDS(int invNum)
        {
            ds = data.ExecuteSQLStatement("SELECT ItemCode FROM LineItems WHERE InvoiceNumber = " +
                        invNum, ref num);
        }

        /// <summary>
        /// Returns a list of all items.
        /// </summary>
        /// <returns></returns>
        public List<Item> GetAllItems()
        {
            List<Item> things = new List<Item>();
            int numb = 0;
            DataSet datas = data.ExecuteSQLStatement("SELECT ItemCode," +
                " Title, Price FROM Products", ref numb);

            for (int i = 0; i < numb; i++)
            {
                Item temp = new Item(datas.Tables[0].Rows[i][0].ToString(), datas.Tables[0].Rows[i][1].ToString(), datas.Tables[0].Rows[i][2].ToString());
                things.Add(temp);
            }
            return things;
        }

        /// <summary>
        /// Gets the current item's code.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public string GetItemCode(int n)
        {
            return ds.Tables[0].Rows[n][0].ToString();
        }

        /// <summary>
        /// Returns item name based on its code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string Name(string code){
            return data.ExecuteScalarSQL("SELECT Title FROM Products WHERE ItemCode = '" + code + "'").ToString();
        }

        /// <summary>
        /// Gets item's price based on it's code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetPrice(string code)
        {
            return data.ExecuteScalarSQL("SELECT Price FROM Products WHERE ItemCode = '" + code + "'").ToString();
        }
    }
}
