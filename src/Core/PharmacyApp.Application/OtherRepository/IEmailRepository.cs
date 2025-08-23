using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.OtherRepository
{
    public interface IEmailRepository
    {
        Task<bool> SendEmail(string mailto, string body, string subject, string mailcc = "",
           byte[] fileattachment = null, string FileName = "");

        string ReadEmailTemplate(string filename);
    }
}
