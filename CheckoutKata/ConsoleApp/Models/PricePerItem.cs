using ConsoleApp.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Models
{
    /// <summary>
    /// Class - it holdes information about Price per Product Type
    /// </summary>
    public class PricePerItem
    {
        public Dictionary<Products, double> PricePerProduct;

        public PricePerItem()
        {
            this.PricePerProduct = new Dictionary<Products, double>();
        }
    }
}
