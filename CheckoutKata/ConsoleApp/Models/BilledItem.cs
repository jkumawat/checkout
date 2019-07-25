using ConsoleApp.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Models
{
    /// <summary>
    /// class - this holds the items which user has confirmed and will be shown in Invoice/Bill
    /// </summary>
    public class BilledItem
    {
        public Products Product;
        public Dictionary<long, double> ItemsWithPrice;
        public Dictionary<long, double> ItemsWithOfferedPrice;
       

        public BilledItem()
        {
            ItemsWithOfferedPrice = new Dictionary<long, double>();
            ItemsWithPrice = new Dictionary<long, double>();
        }

        /// <summary>
        /// Method will provide Total Price for each time 
        /// </summary>
        /// <returns></returns>
        public double TotalPrice()
        {
            return (ItemsWithPrice != null && ItemsWithPrice.Count > 0 ? ItemsWithPrice.Values.Sum() : 0) +
                (ItemsWithOfferedPrice != null && ItemsWithOfferedPrice.Count > 0 ? ItemsWithOfferedPrice.Values.Sum() : 0);
        }
    }
}
