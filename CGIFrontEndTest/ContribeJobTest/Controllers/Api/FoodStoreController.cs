using CGIFrontEndTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CGIFrontEndTest.Controllers.Api
{
    public class FoodStoreController : ApiController
    {
        [HttpGet]
        public dynamic GetAll() //API Endpoint to return all books
        {
            FoodStore foodStore = new FoodStore();
            foodStore.AddDummyData();
            return foodStore.GetAll();
        }

        [HttpGet]
        public async Task<dynamic> GetFoodItems(string searchString) //API Endpoint to return searched books 
        {
            FoodStore foodStore = new FoodStore();
            foodStore.AddDummyData();
            return await foodStore.GetFoodItemsAsync(searchString);
        }

        [HttpPost]
        public dynamic PlaceOrder(IEnumerable<Cart> cart) //API Endpoint to return placed order in cart
        {
            FoodStore foodStore = new FoodStore();
            foodStore.AddDummyData();
            return Cart.PlaceOrder(cart, foodStore);
        }
    }
}
