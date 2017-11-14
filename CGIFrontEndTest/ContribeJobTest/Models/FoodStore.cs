using CGIFrontEndTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CGIFrontEndTest.Models
{
    public class FoodStore : IFoodStoreService
    {
        List<IFoodItem> FoodItems = new List<IFoodItem>();

        public void AddDummyData() //Adding data objects
        {
            FoodItem footItem = new FoodItem("Banana", "Norway", 20, 15);
            FoodItem footItem1 = new FoodItem("Banana", "Sweden", 18, 10);
            FoodItem footItem2 = new FoodItem("Apples", "France", 15.5F, 5);
            FoodItem footItem3 = new FoodItem("Oranges", "France", 15.5F, 5);

            FoodItems.Add(footItem);
            FoodItems.Add(footItem1);
            FoodItems.Add(footItem2);
            FoodItems.Add(footItem3);
        }

        public List<IFoodItem> GetAll() //Display all books
        {
            return FoodItems;
        }

        public Task<IEnumerable<IFoodItem>> GetFoodItemsAsync(string searchString) //Performing search on basis of food item name and country
        {
            searchString = searchString.ToLower();
            var result = FoodItems.Where(i => i.ManufacturerCountry.ToLower().Contains(searchString) || i.Title.ToLower().Contains(searchString));
            return Task.FromResult(result);
        }

    }
}