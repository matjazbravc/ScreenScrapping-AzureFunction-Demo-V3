using Autofac;
using ScreenScrappingAzureFunctionDemoV3.Services;

namespace ScreenScrappingAzureFunctionDemoV3.Modules
{
    public class ServiceRegistrationModule : Module
    {
        /// <inheritdoc />
        /// <summary>
        ///     Add registrations to the container builder.
        /// </summary>
        /// <param name="builder">The container builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ScreenScrappingService>().As<IScreenScrappingService>().InstancePerDependency();
        }
    }
}