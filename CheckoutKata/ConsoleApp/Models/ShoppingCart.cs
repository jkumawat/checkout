using ConsoleApp.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Models
{
    /// <summary>
    /// Class - Shopping car which will hold the info  about Items Customer want to purchase
    /// </summary>
    public class ShoppingCart
    {
        public Dictionary<Products, long> cart;

        public ShoppingCart()
        {
            cart = new Dictionary<Products, long>();
        }
    }
}
