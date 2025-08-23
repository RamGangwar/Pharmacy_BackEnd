using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace PharmacyApp.IOC
{
    public static class MediatRDependencyInjection
    {
        public static IServiceCollection AddMediatRDependencyInjection(this IServiceCollection service)
        {
            service.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            return service;
        }

        
    }
}
