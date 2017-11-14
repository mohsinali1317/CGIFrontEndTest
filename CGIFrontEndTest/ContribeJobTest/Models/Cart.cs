using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CGIFrontEndTest.Models
{
    public class Cart
    {
        public string title { get; set; }

        public string manufacturerCountry { get; set; }

        public string message { get; set; }

        public float amount { get; set; }

        public float price { get; set; }

        public bool fullOrderPlaced { get; set; }


        public static List<Cart> PlaceOrder(IEnumerable<Cart> cart, FoodStore foodStore) //Placing Order in Cart
        {
            List<Cart> result = new List<Cart>();

            foreach (Cart c in cart)
            {
                FoodItem foodItem = foodStore.GetAll().Where(i => i.Title == c.title).FirstOrDefault() as FoodItem;
                if ((foodItem.InStock - c.amount) > -1)
                {
                    c.message = String.Format("You ordered {0} item(s). Order is placed successfully.", c.amount);
                    c.fullOrderPlaced = true;
                }
                else
                {
                    c.message = String.Format("You ordered {0} item(s). Order is placed successfully for {1} copy(s) because more than that are not available.", c.amount, foodItem.InStock);
                    c.amount = foodItem.InStock;
                    c.fullOrderPlaced = false;
                }
                result.Add(c);
            }
            return result;
        }
    }
}