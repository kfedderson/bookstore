using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookStore
{
    class clsSearchLogic
    {

        /// <summary>
        /// SQL queries class
        /// </summary>
        clsSearchSQL sql;


        /// <summary>
        /// Default constructor
        /// </summary>
        public clsSearchLogic()
        {
            sql = new clsSearchSQL();   //Sql query class
        }


        /// <summary>
        /// Method to get a list of all invoices from database
        /// </summary>
        /// <returns>list of invoice objects</returns>
        public List<clsInvoice> GetAllInvoiceData()
        {
            try
            {
                List<clsInvoice> invoiceList = new List<clsInvoice>();
                invoiceList = sql.GetAllInvoiceData();  //cal sql class to get data

                return invoiceList;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Method to get an invoice by its id
        /// </summary>
        /// <param name="id">invoice id</param>
        /// <returns>invoice object</returns>
        public List<clsInvoice> GetInvoiceById(string id)
        {
            try
            {
                List<clsInvoice> invoice = new List<clsInvoice>();
                invoice = sql.GetInvoiceById(id);   //cal sql class to get data

                return invoice;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// method to get a list of invoices by a specific date
        /// </summary>
        /// <param name="date">invoice date</param>
        /// <returns>list of invoice objects</returns>
        public List<clsInvoice> GetAllInvoiceDataByDate(string date)
        {
            try
            {
                List<clsInvoice> invoiceList = new List<clsInvoice>();
                invoiceList = sql.GetAllInvoiceDataByDate(date);    //cal sql class to get data

                return invoiceList;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// method to get a list of invoices by an amount
        /// </summary>
        /// <param name="amount">invoice amount</param>
        /// <returns>list of invoices</returns>
        public List<clsInvoice> GetAllInvoiceDataByAmount(string amount)
        {
            try
            {
                List<clsInvoice> invoiceList = new List<clsInvoice>();
                invoiceList = sql.GetAllInvoiceDataByAmount(amount);    //cal sql class to get data

                return invoiceList;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// method to get a list of invoice ids
        /// </summary>
        public List<clsInvoice> GetInvoiceIdList()
        {
            try
            {
                List<clsInvoice> fullInvoiceList = new List<clsInvoice>();
                fullInvoiceList = sql.GetAllInvoiceData();  //cal sql class to get full list

                List<clsInvoice> invoiceIdList = new List<clsInvoice>();

                //Loop through full list and only pull out invoice number to add to new list
                foreach (var item in fullInvoiceList)
                {
                    invoiceIdList.Add(new clsInvoice { sID = item.sID });
                }
                return invoiceIdList;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// method to get list of invoice ids by date
        /// </summary>
        public List<clsInvoice> GetInvoiceIdListByDate(string date)
        {
            try
            {
                List<clsInvoice> invoiceList = new List<clsInvoice>();
                invoiceList = sql.GetInvoiceIdListByDate(date); //cal sql class to get data

                return invoiceList;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// method to get list of invoice ids by date and amount
        /// </summary>
        public List<clsInvoice> GetInvoiceIdListByDateAndAmount(string date, string amount)
        {
            try
            {
                List<clsInvoice> invoiceList = new List<clsInvoice>();
                invoiceList = sql.GetInvoiceIdListByDateAndAmount(date, amount);    //cal sql class to get data

                return invoiceList;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// method to get a list of invoice ids by amount
        /// </summary>
        /// <param name="amount">invoice amount</param>
        /// <returns>list of invoice objects</returns>
        public List<clsInvoice> GetInvoiceIdListByAmount(string amount)
        {
            try
            {
                List<clsInvoice> invoiceList = new List<clsInvoice>();
                invoiceList = sql.GetInvoiceIdListByAmount(amount); //cal sql class to get data

                return invoiceList;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// method to get a list of unique invoice dates
        /// </summary>
        public List<clsInvoice> GetUniqueInvoiceDates()
        {
            try
            {
                List<clsInvoice> invoiceList = new List<clsInvoice>();
                invoiceList = sql.GetUniqueInvoiceDates();  //cal sql class to get data

                return invoiceList;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// method to get a list of unique invoice amounts
        /// </summary>
        public List<clsInvoice> GetUniqueInvoiceAmounts()
        {
            try
            {
                List<clsInvoice> invoiceList = new List<clsInvoice>();
                invoiceList = sql.GetUniqueInvoiceAmounts();    //cal sql class to get data

                return invoiceList;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// method to get a list of unique amounts by a date
        /// </summary>
        /// <param name="date">invoice date</param>
        /// <returns>list of dollar amounts</returns>
        public List<clsInvoice> GetUniqueAmountsByDate(string date)
        {
            try
            {
                List<clsInvoice> invoiceList = new List<clsInvoice>();
                invoiceList = sql.GetUniqueAmountsByDate(date); //cal sql class to get data

                return invoiceList;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Method to get a list of unique dates by an amount
        /// </summary>
        /// <param name="amount">invoice amount</param>
        /// <returns>list of dates</returns>
        public List<clsInvoice> GetUniqueDatesByAmount(string amount)
        {
            try
            {
                List<clsInvoice> invoiceList = new List<clsInvoice>();
                invoiceList = sql.GetUniqueDatesByAmount(amount);   //cal sql class to get data

                return invoiceList;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Method to get all invoice data for invoices that match given date and amount
        /// </summary>
        /// <param name="date">invoice date</param>
        /// <param name="amount">invoice amount</param>
        /// <returns>invoice object list</returns>
        public List<clsInvoice> GetInvoicesByDateAndAmount(string date, string amount)
        {
            try
            {
                List<clsInvoice> invoiceList = new List<clsInvoice>();
                invoiceList = sql.GetInvoicesByDateAndAmount(date, amount);

                return invoiceList;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
                return null;
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
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }



    /// <summary>
    /// class to hold invoice objects
    /// </summary>
    public class clsInvoice
    {
        public string sID { get; set; }
        public string sDate { get; set; }
        public string sAmount { get; set; }
    }
}
