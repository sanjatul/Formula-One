using FormulaOne.Services.General.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Services.General
{
    public class MaintenanceService : IMaintenanceService
    {
        public void SyncRecord()
        {
            Console.WriteLine("The sync has started");
        }
    }
}
