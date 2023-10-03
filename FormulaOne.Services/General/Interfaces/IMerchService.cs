using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Services.General.Interfaces
{
    public interface IMerchService
    {
        void CreateMerch(Guid driverId);
        void RemoveMerch(Guid driverId);
    }
}
