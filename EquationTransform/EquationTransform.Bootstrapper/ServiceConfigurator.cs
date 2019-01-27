using EquationTransform.Infrastructure;
using EquationTransform.IO.Console;
using EquationTransform.IO.Contract;
using EquationTransform.IO.File;
using EquationTransform.Transformator.Contract;
using EquationTransform.Transformator.SimpleTransformator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EquationTransform.Bootstrapper
{
    public class ServiceConfigurator
    {
        public static ServiceProvider InitializeServiceProvider(IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<EquationConsoleReader>();
            services.AddTransient<EquationConsoleWriter>();

            var inputFilePath = config.GetValue<string>("InputFilePath", null);
            var outputFilePath = config.GetValue<string>("OutputFilePath", null);
            services.AddTransient<IEquationReader>(provider => new EquationFileReader(inputFilePath));
            services.AddTransient<IEquationWriter>(provider => new EquationFileWriter(outputFilePath));

            services.AddTransient<Func<IOType, IEquationReader>>(provider => type =>
            {
                switch (type)
                {
                    case IOType.Console:
                        return provider.GetService<EquationConsoleReader>();
                    case IOType.File:
                        return provider.GetService<EquationFileReader>();
                    default:
                        throw new ArgumentException("Unsupported IO type");
                }
            });

            services.AddTransient<Func<IOType, IEquationWriter>>(provider => type =>
            {
                switch (type)
                {
                    case IOType.Console:
                        return provider.GetService<EquationConsoleWriter>();
                    case IOType.File:
                        return provider.GetService<EquationFileWriter>();
                    default:
                        throw new ArgumentException("Unsupported IO type");
                }
            });

            services.AddSingleton<IEquationTransformator, SimpleEquationTransformator>();

            return services.BuildServiceProvider();
        }

        private static T GetIOService<T>(IOType ioType, IServiceProvider provider)
        {
            switch (ioType)
            {
                case IOType.Console:
                    return provider.GetService<T>();
                case IOType.File:
                    return provider.GetService<T>();
                default:
                    throw new ArgumentException("Unsupported IO type");
            }
        }
    }
}
