using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows;

namespace BookStore
{


    class clsSearchSQL
    {
        /// <summary>
        /// clsDataAccess object
        /// </summary>
        clsDataAccess db = new clsDataAccess();


        /// <summary>
        /// Method to get all invoice data from database
        /// </summary>
        /// <returns>list of invoices</returns>
        public List<clsInvoice> GetAllInvoiceData()
        {
            try
            {
                //Create a DataSet to hold the data
                DataSet ds;

                //create the list to hold invoice objects
                List<clsInvoice> list = new List<clsInvoice>();

                //Number of return values
                int iRet = 0;

                //Get data
                string sSQL = "SELECT * FROM Invoices";

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string id = ds.Tables[0].Rows[i][0].ToString();

                    DateTime dtDate = (DateTime)ds.Tables[0].Rows[i][1];
                    string date = dtDate.ToString("MM/dd/yyyy");

                    decimal amt = (decimal) ds.Tables[0].Rows[i][2];
                    string amount = amt.ToString("$0.00");
                    

                    //Add invoice from each row to the list
                    list.Add(new clsInvoice { sID = id, sDate = date, sAmount = amount });
                }

                return list; //return the list
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Method to get an invoice list that matches given date and amount
        /// </summary>
        /// <param name="date">invoice date</param>
        /// <param name="amount">invoice amount</param>
        /// <returns>invoice object list</returns>
        public List<clsInvoice> GetInvoicesByDateAndAmount(string date, string amount)
        {
            try
            {
                //convert string amount to decimal
                decimal dAmount = Decimal.Parse(amount.Replace("$", ""));

                //Create a DataSet to hold the data
                DataSet ds;

                //create the list to hold invoice objects
                List<clsInvoice> list = new List<clsInvoice>();

                //Number of return values
                int iRet = 0;

                //Get data
                string sSQL = "SELECT * FROM Invoices WHERE TotalAmount = " + dAmount + " AND InvoiceDate = #" + date + "#";

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string id = ds.Tables[0].Rows[i][0].ToString();

                    DateTime dtDate = (DateTime)ds.Tables[0].Rows[i][1];
                    string formatDate = dtDate.ToString("MM/dd/yyyy");

                    decimal amt = (decimal)ds.Tables[0].Rows[i][2];
                    string formatAmount = amt.ToString("$0.00");


                    //Add invoice from each row to the list
                    list.Add(new clsInvoice { sID = id, sDate = formatDate, sAmount = formatAmount });
                }

                return list; //return the list
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Method to get a list of invoice objects that match a certain date
        /// </summary>
        /// <param name="date">invoice date</param>
        /// <returns>list of invoice objects</returns>
        public List<clsInvoice> GetAllInvoiceDataByDate(string date)
        {
            try
            {
                //Create a DataSet to hold the data
                DataSet ds;

                //create the list to hold invoice objects
                List<clsInvoice> list = new List<clsInvoice>();

                //Number of return values
                int iRet = 0;

                //Get data
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + date + "#";

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string returnedId = ds.Tables[0].Rows[i][0].ToString();

                    DateTime dtDate = (DateTime)ds.Tables[0].Rows[i][1];
                    string returnedDate = dtDate.ToString("MM/dd/yyyy");

                    decimal amt = (decimal)ds.Tables[0].Rows[i][2];
                    string amount = amt.ToString("$0.00");


                    //Add invoice from each row to the list
                    list.Add(new clsInvoice { sID = returnedId, sDate = returnedDate, sAmount = amount });
                }

                return list; //return the list
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Method to get a list of invoice objects by given amount
        /// </summary>
        /// <param name="amount">invoice amount</param>
        /// <returns>list of invoice objects</returns>
        public List<clsInvoice> GetAllInvoiceDataByAmount(string amount)
        {
            try
            {
                //convert string amount to decimal
                decimal dAmount = Decimal.Parse(amount.Replace("$", ""));

                //Create a DataSet to hold the data
                DataSet ds;

                //create the list to hold invoice objects
                List<clsInvoice> list = new List<clsInvoice>();

                //Number of return values
                int iRet = 0;

                //Get data
                string sSQL = "SELECT * FROM Invoices WHERE TotalAmount = " + dAmount;

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string returnedId = ds.Tables[0].Rows[i][0].ToString();

                    DateTime dtDate = (DateTime)ds.Tables[0].Rows[i][1];
                    string returnedDate = dtDate.ToString("MM/dd/yyyy");

                    decimal amt = (decimal)ds.Tables[0].Rows[i][2];
                    string sAmt = amt.ToString("$0.00");


                    //Add invoice from each row to the list
                    list.Add(new clsInvoice { sID = returnedId, sDate = returnedDate, sAmount = sAmt });
                }

                return list; //return the list
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Method to get an invoice object by an id
        /// </summary>
        /// <param name="id">invoice id</param>
        /// <returns>list of invoice objects</returns>
        public List<clsInvoice> GetInvoiceById(string id)
        {
            try
            {
                //convert string id to int
                Int32.TryParse(id, out int iId);
                
                //Create a DataSet to hold the data
                DataSet ds;

                //create the list to hold invoice objects
                List<clsInvoice> list = new List<clsInvoice>();

                //Number of return values
                int iRet = 0;

                //Get data
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNumber = " + iId;

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string returnedId = ds.Tables[0].Rows[i][0].ToString();

                    DateTime dtDate = (DateTime)ds.Tables[0].Rows[i][1];
                    string date = dtDate.ToString("MM/dd/yyyy");

                    decimal amt = (decimal)ds.Tables[0].Rows[i][2];
                    string amount = amt.ToString("$0.00");


                    //Add invoice from each row to the list
                    list.Add(new clsInvoice { sID = returnedId, sDate = date, sAmount = amount });
                }

                return list; //return the list
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Method to get a list of invoice id's by a given date
        /// </summary>
        /// <param name="sDate">invoice date</param>
        /// <returns>list of invoice id's</returns>
        public List<clsInvoice> GetInvoiceIdListByDate(string sDate)
        {
            try
            {               
                //Create a DataSet to hold the data
                DataSet ds;

                //create the list to hold invoice objects
                List<clsInvoice> list = new List<clsInvoice>();

                //Number of return values
                int iRet = 0;

                //Get data
                string sSQL = "SELECT InvoiceNumber FROM Invoices WHERE InvoiceDate = #" + sDate + "#";

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string id = ds.Tables[0].Rows[i][0].ToString();

                    //Add invoice from each row to the list
                    list.Add(new clsInvoice { sID = id });
                }

                return list; //return the list
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Method to get a list of invoice id's by given amount
        /// </summary>
        /// <param name="amount">invoice amount</param>
        /// <returns>list of invoice id's</returns>
        public List<clsInvoice> GetInvoiceIdListByAmount(string amount)
        {
            try
            {
                //convert string amount to decimal
                decimal dAmount = Decimal.Parse(amount.Replace("$", ""));

                //Create a DataSet to hold the data
                DataSet ds;

                //create the list to hold invoice objects
                List<clsInvoice> list = new List<clsInvoice>();

                //Number of return values
                int iRet = 0;

                //Get data
                string sSQL = "SELECT InvoiceNumber FROM Invoices WHERE TotalAmount = " + dAmount;

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string id = ds.Tables[0].Rows[i][0].ToString();

                    //Add invoice from each row to the list
                    list.Add(new clsInvoice { sID = id });
                }

                return list; //return the list
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Method to get an invoice id list filtered by date and amount
        /// </summary>
        /// <param name="sDate">invoice date</param>
        /// <param name="amount">invoice amount</param>
        /// <returns>list of invoice ids</returns>
        public List<clsInvoice> GetInvoiceIdListByDateAndAmount(string sDate, string amount)
        {
            try
            {
                //convert string amount to decimal
                decimal dAmount = Decimal.Parse(amount.Replace("$", ""));

                //Create a DataSet to hold the data
                DataSet ds;

                //create the list to hold invoice objects
                List<clsInvoice> list = new List<clsInvoice>();

                //Number of return values
                int iRet = 0;

                //Get data
                string sSQL = "SELECT InvoiceNumber FROM Invoices WHERE TotalAmount = " + dAmount + " AND InvoiceDate = #" + sDate + "#";

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string id = ds.Tables[0].Rows[i][0].ToString();

                    //Add invoice from each row to the list
                    list.Add(new clsInvoice { sID = id });
                }

                return list; //return the list
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Method to get a list of unique invoice dates
        /// </summary>
        /// <returns>list of invoice dates</returns>
        public List<clsInvoice> GetUniqueInvoiceDates()
        {
            try
            {
                //Create a DataSet to hold the data
                DataSet ds;

                //create the list to hold invoice objects
                List<clsInvoice> list = new List<clsInvoice>();

                //Number of return values
                int iRet = 0;

                //Get data
                string sSQL = "SELECT DISTINCT InvoiceDate FROM Invoices";

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DateTime dtDate = (DateTime)ds.Tables[0].Rows[i][0];
                    string date = dtDate.ToString("MM/dd/yyyy");
                    
                    //Add invoice from each row to the list
                    list.Add(new clsInvoice { sDate = date });
                }

                return list; //return the list
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        
        /// <summary>
        /// Method to get a list of unique amounts
        /// </summary>
        /// <returns>invoice amounts</returns>
        public List<clsInvoice> GetUniqueInvoiceAmounts()
        {
            try
            {
                //Create a DataSet to hold the data
                DataSet ds;

                //create the list to hold invoice objects
                List<clsInvoice> list = new List<clsInvoice>();

                //Number of return values
                int iRet = 0;

                //Get data
                string sSQL = "SELECT DISTINCT TotalAmount FROM Invoices";

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    decimal amt = (decimal) ds.Tables[0].Rows[i][0];
                    string amount = amt.ToString("$0.00");

                    //Add invoice from each row to the list
                    list.Add(new clsInvoice { sAmount = amount });
                }

                return list; //return the list
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Method to get a list of unique amounts by given date
        /// </summary>
        /// <param name="date">invoice date</param>
        /// <returns>list of amounts</returns>
        public List<clsInvoice> GetUniqueAmountsByDate(string date)
        {
            try
            {
                //Create a DataSet to hold the data
                DataSet ds;

                //create the list to hold invoice objects
                List<clsInvoice> list = new List<clsInvoice>();

                //Number of return values
                int iRet = 0;

                //Get data
                string sSQL = "SELECT DISTINCT TotalAmount FROM Invoices WHERE InvoiceDate = #" + date + "#";

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    decimal amt = (decimal)ds.Tables[0].Rows[i][0];
                    string amount = amt.ToString("$0.00");

                    //Add invoice from each row to the list
                    list.Add(new clsInvoice { sAmount = amount });
                }

                return list; //return the list
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Method to get a list of unique dates by given amount
        /// </summary>
        /// <param name="amount">invoice amount</param>
        /// <returns>list of dates</returns>
        public List<clsInvoice> GetUniqueDatesByAmount(string amount)
        {
            try
            {
                //convert string amount to decimal
                decimal dAmount = Decimal.Parse(amount.Replace("$", ""));

                //Create a DataSet to hold the data
                DataSet ds;

                //create the list to hold invoice objects
                List<clsInvoice> list = new List<clsInvoice>();

                //Number of return values
                int iRet = 0;

                //Get data
                string sSQL = "SELECT DISTINCT InvoiceDate FROM Invoices WHERE TotalAmount = " + dAmount;

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //string date = ds.Tables[0].Rows[i][0].ToString();
                    DateTime dtDate = (DateTime)ds.Tables[0].Rows[i][0];
                    string date = dtDate.ToString("MM/dd/yyyy");

                    //Add invoice from each row to the list
                    list.Add(new clsInvoice { sDate = date });
                }

                return list; //return the list
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
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
    }
}
