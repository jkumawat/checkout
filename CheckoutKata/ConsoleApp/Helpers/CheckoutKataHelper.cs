using ConsoleApp.Enums;
using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp.Helpers
{
    public static class CheckoutKataHelper
    {
        /// <summary>
        /// Add Item in the Shopping cart
        /// </summary>
        /// <param name="value"> Entry from User in Console - Product code</param>
        /// <param name="ShoppingCart">Shopping card for User</param>
        /// <returns></returns>
        public static ShoppingCart AddItemInShoppingCart(double value, ShoppingCart shoppingCart)
        {
            Products selectedProduct = Enum.Parse<Products>(value.ToString());
            if (shoppingCart.cart.Keys.Contains(selectedProduct))
            {
                shoppingCart.cart[selectedProduct] = ++shoppingCart.cart[selectedProduct];
            }
            else
            {
                shoppingCart.cart.Add(selectedProduct, 1);
            }

            return shoppingCart;
        }

        /// <summary>
        /// Generate bill with applying Weekly offers to added items in the shopping cart
        /// </summary>
        /// <param name="ShoppingCart"></param>
        /// <param name="offer"></param>
        public static Bill GenerateBill(Dictionary<Products, long> ShoppingCart, WeeklyOffer offer, PricePerItem productPrices)
        {
            // Generate bill object 
            Bill custBill = new Bill();
            
            List<BilledItem> billedItems = new List<BilledItem>();

            foreach (Products item in ShoppingCart.Keys)
            {

                BilledItem billItem = new BilledItem();

                billItem.Product = item;

                Dictionary<long, double> offeredPriceForItem = offer.Offer.FirstOrDefault(kvp => kvp.Key == item).Value;

                long productCountInBucket = ShoppingCart[item];

                // apply Offer 
                long howManyOnOfferedPrice = 0;

                offeredPriceForItem?.Keys?.OrderByDescending(v => v).ToList().ForEach(key => {
                    if (offeredPriceForItem != null && productCountInBucket >= key)
                    {
                        howManyOnOfferedPrice = productCountInBucket - (productCountInBucket % key);
                        billItem.ItemsWithOfferedPrice.Add(howManyOnOfferedPrice, ((howManyOnOfferedPrice/key) * offeredPriceForItem[key]));
                    }
                });

                long howManyWithoutOfferedPrice = productCountInBucket - billItem.ItemsWithOfferedPrice.Keys.Sum();

                if (howManyWithoutOfferedPrice > 0)
                {
                    billItem.ItemsWithPrice.Add(howManyWithoutOfferedPrice, howManyWithoutOfferedPrice * productPrices.PricePerProduct[item]);
                }

                // add billed items in the bill array
                billedItems.Add(billItem);
            }

            custBill.Products = billedItems;

            return custBill;
        }

        /// <summary>
        /// Print Generate Bill
        /// </summary>
        /// <param name="customerBill"></param>
        public static void PrintBill(Bill customerBill)
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("\n\n");

            Console.WriteLine("==========================");
            Console.WriteLine("Generate System Invoice");
            Console.WriteLine("==========================");

            customerBill.Products.ForEach(item => {
                StringBuilder strBuild = new StringBuilder();

                strBuild.Append(item.Product);
                strBuild.Append(" --- ");
                strBuild.Append("Sale (" + String.Join(',', item.ItemsWithOfferedPrice.Keys) + ")");
                strBuild.Append("---");
                strBuild.Append(String.Join(',', item.ItemsWithPrice.Keys));
                strBuild.Append("---");
                strBuild.Append(String.Join(',', item.TotalPrice()));


                Console.WriteLine(strBuild.ToString());
            });

            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Payable Amount ==> " + customerBill.PayableAmount());
            Console.WriteLine("------------------------------------------------");
        }

        /// <summary>
        /// Generate Message for User to know the possible values he can enter.
        /// </summary>
        /// <returns></returns>
        public static string GenerateInputMessage()
        {
            StringBuilder strBuild = new StringBuilder();

            strBuild.Append("Enter : ");

            foreach (Products item in Enum.GetValues(typeof(Products)))
            {
                strBuild.Append((long)item + "- " + item.ToString() + ", ");
            }

            strBuild.Append(" 0 - to get Bill ==> ");

            return strBuild.ToString();
        }

        /// <summary>
        /// Validate input provided by User in Console
        /// </summary>
        /// <returns></returns>
        public static long ValidateInput()
        {
            int result;
            String Result = Console.ReadLine();

            while (!(int.TryParse(Result, out result) && (result == 0 || Enum.IsDefined(typeof(Products), result))))
            {
                Console.Write("Not a valid number, try again. ==>");

                Result = Console.ReadLine();
            }

            return result;
        }

        public static bool ValidateInputString(string input)
        {
            int result;

            return int.TryParse(input, out result) && (result == 0 || Enum.IsDefined(typeof(Products), result));
        }
    }
}
