using ConsoleApp.Enums;
using ConsoleApp.Helpers;
using ConsoleApp.Models;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ShoppingCart shoppingBucket = new ShoppingCart();

            //Weekly Offer
            WeeklyOffer offer = new WeeklyOffer();
            offer.Offer.Add(Products.Apple, new Dictionary<long, double> { { 2, 45 } });
            offer.Offer.Add(Products.Banana, new Dictionary<long, double> { { 3, 130 } });

            //Product Price
            PricePerItem pricePerItem = new PricePerItem();
            pricePerItem.PricePerProduct.Add(Products.Apple, 30);
            pricePerItem.PricePerProduct.Add(Products.Banana, 50);
            pricePerItem.PricePerProduct.Add(Products.Peach, 60);

            Console.WriteLine("\n\n");
            Console.WriteLine("=====================================");
            Console.WriteLine("Shopping Cart Kata");
            Console.WriteLine("=====================================");
            Console.WriteLine("\n\n");

            double value = 0;
            string inputMessage = CheckoutKataHelper.GenerateInputMessage();

            do
            {
                Console.Write(inputMessage);
                value = CheckoutKataHelper.ValidateInput();

                if (value != 0)
                {

                    shoppingBucket = CheckoutKataHelper.AddItemInShoppingCart(value, shoppingBucket);
                }

            } while (value != 0);


            //Add in the bill
            Bill custBill = CheckoutKataHelper.GenerateBill(shoppingBucket.cart, offer, pricePerItem);

            //Print bill.
            CheckoutKataHelper.PrintBill(custBill, offer, pricePerItem);
        }
    }
}
