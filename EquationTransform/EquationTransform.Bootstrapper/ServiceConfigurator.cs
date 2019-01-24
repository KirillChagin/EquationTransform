using EquationTransform.Infrastructure;
using EquationTransform.IO.Console;
using EquationTransform.IO.Contract;
using EquationTransform.IO.File;
using EquationTransform.Transformator.Contract;
using EquationTransform.Transformator.SimpleTransformator;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EquationTransform.Bootstrapper
{
    public class ServiceConfigurator
    {
        public static ServiceProvider InitializeServiceProvider(IServiceCollection services)
        {
            services.AddTransient<EquationConsoleIO>();
            services.AddTransient<EquationFileIO>();

            services.AddTransient<Func<IOType, IEquationReader>>(provider => type =>
            {
                return GetIOService<IEquationReader>(type, provider);
            });

            services.AddTransient<Func<IOType, IEquationWriter>>(provider => type =>
            {
                return GetIOService<IEquationWriter>(type, provider);
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
