using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmacyApp.Application.OtherRepository;
using PharmacyApp.Data.OtherRepository;
using PharmacyApp.Domain.Helper;
namespace PharmacyApp.IOC
{
    public static class AddOtherServices
    {   
        public static void AddGEOLocation(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GEOLocationOptions>(configuration.GetSection("GEOLocationConfig"));
            services.AddScoped<IGeoLocationRepository, GeoLocationRepository>();
        }
    }
}
