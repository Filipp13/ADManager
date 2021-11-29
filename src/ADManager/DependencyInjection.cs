using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace ADManager
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddADManagment(
         this IServiceCollection services,
         IConfiguration configuration,
         ADManagerSecurityOptions aDManagerSecurityOptions)
        {
            var options = configuration
                      .GetSection(ADManagerOptions.SectionName)
                      .Get<ADManagerOptions>();

            if (options is null)
            {
                throw new InvalidOperationException(
                    $"Configuration options {ADManagerOptions.SectionName} are absent");
            }

            services.Configure<ADManagerOptions>(configuration.GetSection(ADManagerOptions.SectionName));

            if (aDManagerSecurityOptions is null)
            {
                throw new InvalidOperationException(
                    "ADManagerSecurityOptions options are absent");
            }

            Options.Create(aDManagerSecurityOptions);

            return services.AddScoped<IADManager, ADManager>();
        }
    }
}
