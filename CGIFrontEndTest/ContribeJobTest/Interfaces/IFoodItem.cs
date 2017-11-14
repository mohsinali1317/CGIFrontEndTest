using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGIFrontEndTest.Interfaces
{
    public interface IFoodItem
    {
        string Title { get; }
        string ManufacturerCountry { get; }
        float Price { get; }
        int InStock { get; }
    }
}
