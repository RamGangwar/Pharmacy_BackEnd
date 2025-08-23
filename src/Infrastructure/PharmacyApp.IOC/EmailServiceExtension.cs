using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using PharmacyApp.Data.OtherRepository;
using PharmacyApp.Application.OtherRepository;
using PharmacyApp.Domain.Helper;

namespace PharmacyApp.IOC
{
    public static class EmailServiceExtension
    {
        public static void AddEmailService(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<EmailNotificationOptions>(configuration.GetSection("EmailConfigration"));            
            services.AddScoped<IEmailRepository, EmailRepository>();
        }
    }
}
