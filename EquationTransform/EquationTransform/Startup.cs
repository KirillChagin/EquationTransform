using EquationTransform.Bootstrapper;
using Microsoft.Extensions.DependencyInjection;

namespace EquationTransform
{
    public static class Startup
    {
        public static ServiceProvider Configure()
        {
            var serviceCollection = new ServiceCollection();

            return ServiceConfigurator.InitializeServiceProvider(serviceCollection);
        }
    }
}
