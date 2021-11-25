using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ADManager
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddADManagment(
         this IServiceCollection services,
         IConfiguration configuration)
        {
            var options = configuration
                      .GetSection(ADManagerOptions.SectionName)
                      .Get<ADManagerOptions>();

            if (options is null)
            {
                throw new InvalidOperationException(
                    $"Configuration options {ADManagerOptions.SectionName} is absent");
            }

            services.Configure<ADManagerOptions>(configuration.GetSection(ADManagerOptions.SectionName));

            return services.AddScoped<IADManager, ADManager>();
        }
    }
}
