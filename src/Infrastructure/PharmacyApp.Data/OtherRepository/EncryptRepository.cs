using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using PharmacyApp.Application.OtherRepository;
using PharmacyApp.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Data.OtherRepository
{
    public class EncryptRepository : IEncryptRepository
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly string _key;

        public EncryptRepository(IDataProtectionProvider dataProtectionProvider,
            IOptionsMonitor<EncryptOptions> options)
        {
            _dataProtectionProvider = dataProtectionProvider ?? throw new ArgumentNullException(nameof(dataProtectionProvider));
            _key = options.CurrentValue?.Key ?? throw new ArgumentNullException(nameof(EncryptOptions));
        }

        public string Encrypt(string str)
        {
            var protector = _dataProtectionProvider.CreateProtector(_key);
            return protector.Protect(str);
        }

        public string Decrypt(string encrypted)
        {
            var protector = _dataProtectionProvider.CreateProtector(_key);
            return protector.Unprotect(encrypted);
        }
    }
}
