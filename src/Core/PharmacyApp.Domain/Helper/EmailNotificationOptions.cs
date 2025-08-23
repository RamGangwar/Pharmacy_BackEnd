using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Domain.Helper
{
    public class EmailNotificationOptions
    {
        public string Fromemail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public bool SSL { get; set; }
        public int Port { get; set; }
        public string SupportEmailAddress { get; set; }
    }
}
