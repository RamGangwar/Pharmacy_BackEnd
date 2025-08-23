using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.OtherRepository
{
    public interface IEncryptRepository
    {
        string Encrypt(string str);
        string Decrypt(string encrypted);
    }
}
