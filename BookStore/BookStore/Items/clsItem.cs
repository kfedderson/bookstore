using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace BookStore.Items
{
    class clsItem : INotifyPropertyChanged
    {
        #region Attributes

        String _ItemCode;
        String _Title;
        String _Author;
        String _Price;


        /// <summary>
        /// Return ItemCode from database
        /// </summary>
        public String ItemCode
        {
            get { return _ItemCode; }
            set
            {
                _ItemCode = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ItemCode"));
                }
            }
        }

        /// <summary>
        /// Return string title from database
        /// </summary>
        public String Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Title"));
                }
            }
        }

        /// <summary>
        /// Return string author from database
        /// </summary>
        public String Author
        {
            get { return _Author; }
            set
            {
                _Author = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Author"));
                }
            }
        }


        /// <summary>
        /// Price from database
        /// </summary>
        public String Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Price"));
                }
            }
        }

        #endregion

        #region Methods
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Override string to display Item Code, Book Title, and Author
        /// </summary>
        /// <returns>Item Code, Title, Author</returns>
        public override string ToString()
        {
            return ItemCode + " - " + Title + Author;
        }


        #endregion

    }
}
