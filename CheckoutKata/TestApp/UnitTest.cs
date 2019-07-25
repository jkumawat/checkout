using ConsoleApp.Enums;
using ConsoleApp.Helpers;
using ConsoleApp.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        private WeeklyOffer offer;
        private ShoppingCart shoppingCart;
        private PricePerItem pricePerItem;

        [SetUp]
        public void Setup()
        {
            shoppingCart = new ShoppingCart();

            //Weekly Offer
            offer = new WeeklyOffer();            
            offer.Offer.Add(Products.Apple, new Dictionary<long, double> { { 2, 45 } });
            offer.Offer.Add(Products.Banana, new Dictionary<long, double> { { 3, 130 } });

            //Product Price
            pricePerItem = new PricePerItem();            
            pricePerItem.PricePerProduct.Add(Products.Apple, 30);
            pricePerItem.PricePerProduct.Add(Products.Banana, 50);
            pricePerItem.PricePerProduct.Add(Products.Peach, 60);            
        }

        [TestCase(3,4,1)]
        [Test]
        public void TestWithOfferApplied(long appleCount, long bananaCount, long peachCount)
        {
            shoppingCart= CheckoutKataHelper.AddItemInShoppingCart(appleCount, shoppingCart);
            shoppingCart = CheckoutKataHelper.AddItemInShoppingCart(bananaCount, shoppingCart);
            shoppingCart = CheckoutKataHelper.AddItemInShoppingCart(peachCount, shoppingCart);

            Bill invoice= CheckoutKataHelper.GenerateBill(shoppingCart.cart, offer, pricePerItem);

            // Apple - 2 (45) + 1 (30) = 75 
            // Banan - 3 (130) + 1 (50) = 180
            // peach - 1 (60) = 60

            Assert.AreEqual(315, invoice.PayableAmount());
        }

        [TestCase(3, 4, 1)]
        [Test]
        public void TestWithOfferAppliedFailed(long appleCount, long bananaCount, long peachCount)
        {
            shoppingCart = CheckoutKataHelper.AddItemInShoppingCart(appleCount, shoppingCart);
            shoppingCart = CheckoutKataHelper.AddItemInShoppingCart(bananaCount, shoppingCart);
            shoppingCart = CheckoutKataHelper.AddItemInShoppingCart(peachCount, shoppingCart);

            Bill invoice = CheckoutKataHelper.GenerateBill(shoppingCart.cart, offer, pricePerItem);

            // Apple - 2 (45) + 1 (30) = 75 
            // Banan - 3 (130) + 1 (50) = 180
            // peach - 1 (60) = 60

            Assert.AreNotEqual(100, invoice.PayableAmount());
        }

        [TestCase(1, 2, 3)]
        [Test]
        public void TestWithoutAnyOffer(long appleCount, long bananaCount, long peachCount)
        {
            shoppingCart = CheckoutKataHelper.AddItemInShoppingCart(appleCount, shoppingCart);
            shoppingCart = CheckoutKataHelper.AddItemInShoppingCart(bananaCount, shoppingCart);
            shoppingCart = CheckoutKataHelper.AddItemInShoppingCart(peachCount, shoppingCart);

            Bill invoice = CheckoutKataHelper.GenerateBill(shoppingCart.cart, offer, pricePerItem);

            // Apple - 1 (30) = 30 
            // Banan - 2 (50) = 100
            // peach - 3 (60) = 180

            Assert.AreEqual(310, invoice.PayableAmount());
        }
    }
}