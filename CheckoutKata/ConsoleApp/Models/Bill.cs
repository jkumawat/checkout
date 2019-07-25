using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp.Models
{
    /// <summary>
    /// Class - presents data for Invoice/Bill
    /// </summary>
    public class Bill
    {

        public List<BilledItem> Products;

        public Bill()
        {
            Products = new List<BilledItem>();
        }

        /// <summary>
        /// Method provide Total Payable amount
        /// </summary>
        /// <returns></returns>
        public double PayableAmount()
        {
            return Products != null && Products.Count > 0 ? Products.Sum(item => item.TotalPrice()) : 0;
        }
    }
}
