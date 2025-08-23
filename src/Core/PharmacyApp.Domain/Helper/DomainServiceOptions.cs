using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Domain.Helper
{
    public class DomainServiceOptions
    {
        public string ERPDomain { get; set; }
        public string WebAPIDomain { get; set; }
        public string ForgotPassword { get; set; }
        public int ForgotPasswordLinkExpireInMinute { get; set; }
    }
}
