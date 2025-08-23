using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.IOC
{
    public static class SerilogDependencyInjection
    {
        public static WebApplicationBuilder AddSerilogDependencyInjection(this WebApplicationBuilder builder)
        {

            var _logger = new LoggerConfiguration()             
             .ReadFrom.Configuration(builder.Configuration)
             .CreateLogger();
            builder.Logging.AddSerilog(_logger);

            return builder;
        }
    }
}
