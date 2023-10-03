using FormulaOne.Services.General.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Services.General
{
    public class EmailService : IEmailService
    {
        public void SendGettingStartedEmail(string email, string name)
        {
            Console.WriteLine($"This will send a getting started email to {name} using the following email {email}");
        }

        public void SendWelcomeEmail(string email, string name)
        {
            Console.WriteLine($"This will send a welcome email to {name} using the following email {email}");
        }
    }
}
