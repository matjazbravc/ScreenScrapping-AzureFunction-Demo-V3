using Autofac;
using Autofac.Extensions.DependencyInjection.AzureFunctions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ScreenScrappingAzureFunctionDemoV3.Modules;
using ScreenScrappingAzureFunctionDemoV3.Services.Settings;

[assembly: FunctionsStartup(typeof(ScreenScrappingAzureFunctionDemoV3.Startup))]

namespace ScreenScrappingAzureFunctionDemoV3
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder
                .UseAppSettings()
                .UseLogger(ConfigureLogger)
                .UseAutofacServiceProviderFactory(ConfigureContainer);
        }

        private void ConfigureLogger(ILoggingBuilder builder, IConfiguration config)
        {
            builder.AddConfiguration(config.GetSection("Logging"));
        }

        private void ConfigureContainer(ContainerBuilder builder)
        {
            // Autofac - register app settings
            builder.Register(activator =>
                {
                    var config = activator.Resolve<IConfiguration>();
                    var settings = new FunctionSettings();
                    config.GetSection(nameof(FunctionSettings)).Bind(settings);
                    return settings;
                })
                .AsSelf()
                .SingleInstance();

            // Autofac - register functions
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .InNamespace("ScreenScrappingAzureFunctionDemoV3.Functions")
                .AsSelf()
                .InstancePerTriggerRequest();

            // Autofac - register services
            builder.RegisterModule(new ServiceRegistrationModule());
        }
    }
}
