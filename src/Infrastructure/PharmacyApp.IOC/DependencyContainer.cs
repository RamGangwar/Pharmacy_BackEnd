using Castle.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PharmacyApp.Application.OtherRepository;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Data.OtherRepository;
using PharmacyApp.Data.Repository;
using PharmacyApp.Data.UnitOfWork;
using PharmacyApp.Domain.Helper;

namespace PharmacyApp.IOC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddServiceDependency( this IServiceCollection services, WebApplicationBuilder builder)
         {

            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddScoped<IBaseUnitOfWork, BaseUnitOfWork>();
            #region Repositories 
            services.AddHttpContextAccessor();
            services.AddSingleton<IUserProvider, UserProvider>();
            services.Configure<DomainServiceOptions>(builder.Configuration.GetSection("Domain"));
            //services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IJWTService, JWTService>();
            #endregion

            return services;
        }
    }
}
