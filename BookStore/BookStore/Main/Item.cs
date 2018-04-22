using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
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
        public decimal price;
        /// <summary>
        /// The string version of price. (Has a $ and is
        /// used by the datagrid.)
        /// </summary>
        public string sPrice { get; set; }
        /// <summary>
        /// The title of the book or name of the item.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="code">Item's code</param>
        /// <param name="title">Book title or object name</param>
        /// <param name="cost">Price of a single unit</param>
        public Item(string code, string title, string cost)
        {
            try
            {
                itemCode = code;
                name = title;
                decimal temp;

                if (!Decimal.TryParse(cost, out temp))
                {
                    //There was a problem.
                    Console.WriteLine("ItemCode: " + itemCode + "\nName: " + name +
                        "\nDidn't have a valid price.");
                    price = 0.00M;
                }
                else
                {
                    price = temp;
                    sPrice = "$" + price.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// How the object is as a string. (Returns
        /// the item's name.)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name;
        }
    }
}
