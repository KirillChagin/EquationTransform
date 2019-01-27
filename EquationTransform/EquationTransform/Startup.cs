using EquationTransform.Bootstrapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace EquationTransform
{
    public static class Startup
    {
        public static ServiceProvider Configure()
        {
            var serviceCollection = new ServiceCollection();

            var dict = new Dictionary<string, string>
            {
                {"InputFilePath", "value1"},
                {"OutputFilePath", "value2"}
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(dict)
                .Build();

            RegisterEquationTransformManager(serviceCollection);

            return ServiceConfigurator.InitializeServiceProvider(serviceCollection, config);
        }

        private static void RegisterEquationTransformManager(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<EquationTransformManager>();
        }
    }
}
