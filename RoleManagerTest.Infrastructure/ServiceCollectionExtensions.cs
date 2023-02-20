using Microsoft.Extensions.DependencyInjection;
using RoleManagerTest.Domain;

namespace RoleManagerTest.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStorage(this IServiceCollection services)
        {
            services.AddRepository<RoleForUser, string>(settings => {
                settings.WithBlobStorage(x =>
                {
                    x.ConnectionString = "";
                    x.ContainerName = nameof(RoleForUser);
                });
            });
            services.AddRepository<ServiceRegistry, string>(settings => {
                settings.WithBlobStorage(x =>
                {
                    x.ConnectionString = "";
                    x.ContainerName = nameof(ServiceRegistry);
                });
            });
            return 
                services;
        }
    }
}