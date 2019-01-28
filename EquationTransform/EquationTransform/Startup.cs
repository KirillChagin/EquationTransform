using EquationTransform.Bootstrapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;

namespace EquationTransform
{
    public static class Startup
    {
        public static ServiceProvider Configure(string inputFilePath, string outFilePath)
        {
            var serviceCollection = new ServiceCollection();  

            var dict = new Dictionary<string, string>
            {
                {"InputFilePath", inputFilePath},
                {"OutputFilePath", outFilePath}
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
