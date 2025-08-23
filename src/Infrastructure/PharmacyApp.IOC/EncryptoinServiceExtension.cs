using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmacyApp.Application.OtherRepository;
using PharmacyApp.Data.OtherRepository;
using PharmacyApp.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacyApp.IOC
{
    public static class EncryptoinServiceExtension
    {
        public static void AddEncryptionService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDataProtection();
            services.Configure<EncryptOptions>(configuration.GetSection("Encryption"));
            services.AddScoped<IEncryptRepository, EncryptRepository>();

        }
    }
}
