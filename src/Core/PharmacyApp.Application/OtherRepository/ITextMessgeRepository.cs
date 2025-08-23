using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.OtherRepository
{
    public interface ITextMessgeRepository
    {
       // string SendSms(MessageModel message);
        bool ValidatePhone(string phone);
    }
}
