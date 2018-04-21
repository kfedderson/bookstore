using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookStore
{
    /// <summary>
    /// Each obj has the info for one item.
    /// </summary>
    class Item
    {
        /// <summary>
        /// The item's unique code. Ex: AQ
        /// </summary>
        public string itemCode;
        /// <summary>
        /// The cost of one of this item.
        /// </summary>
        public double price;
        /// <summary>
        /// The title of the book or name of the item.
        /// </summary>
        public string name;

        public Item(string code, string title, string cost)
        {
            try
            {
                itemCode = code;
                name = title;

                if (!Double.TryParse(cost, out price))
                {
                    //There was a problem.
                    Console.WriteLine("ItemCode: " + itemCode + "\nName: " + name +
                        "\nDidn't have a valid price.");
                    price = 0.00;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


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

        public override string ToString()
        {
            return name;
        }
    }
}
