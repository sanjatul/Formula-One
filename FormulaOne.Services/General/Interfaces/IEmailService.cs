using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Services.General.Interfaces
{
    public interface IEmailService
    {
        void SendWelcomeEmail(string email,string name);
        void SendGettingStartedEmail(string email,string name);
    }
}
