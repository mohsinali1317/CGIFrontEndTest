using CGIFrontEndTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CGIFrontEndTest.Models
{
    public class FoodItem : IFoodItem
    {
        public FoodItem(string title, string manufacturerCountry, float price, int inStock)
        {
            Title = title;
            ManufacturerCountry = manufacturerCountry;
            Price = price;
            InStock = inStock;
        }

        public string Title { get; set; }

        public string ManufacturerCountry { get; set; }

        public float Price { get; set; }

        public int InStock { get; set; }

    }
}