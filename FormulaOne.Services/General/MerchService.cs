using FormulaOne.Services.General.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Services.General
{
    public class MerchService : IMerchService
    {
        public void CreateMerch(Guid driverId)
        {
            Console.WriteLine($"this will create merch for driver {driverId}");
        }

        public void RemoveMerch(Guid driverId)
        {
            Console.WriteLine($"this will create merch for driver {driverId}");
        }
    }
}
