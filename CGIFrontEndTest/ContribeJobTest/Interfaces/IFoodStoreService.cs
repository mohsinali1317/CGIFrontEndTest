using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace CGIFrontEndTest.Interfaces
{
    public interface IFoodStoreService
    {
        Task<IEnumerable<IFoodItem>> GetFoodItemsAsync(string searchString);
    }
}