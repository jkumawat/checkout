using ConsoleApp.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Models
{
    /// <summary>
    /// Class which will hold Weekly offer on Products
    /// </summary>
    public class WeeklyOffer
    {
        public Dictionary<Products, Dictionary<long, double>> Offer;

        public WeeklyOffer()
        {
            Offer = new Dictionary<Products, Dictionary<long, double>>();
        }
    }
}
