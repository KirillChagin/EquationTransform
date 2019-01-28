using System;
using Microsoft.Extensions.DependencyInjection;
using EquationTransform.CommandLineHelper;

namespace EquationTransform
{
    class Program
    {
        public const int SuccessCode = 0;
        public const int ErrorCode = 1;

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (o, e) =>
            {
                ExitWithError("Oops! It was unexpected.");
            };

            var validationResult = CommandLineArgumentsValidator.GetCommandLineResult(args);

            if (!validationResult.Success)
            {
                ExitWithError(validationResult.ErrorMessage);
            }

            var serviceProvider = Startup.Configure(validationResult.InputFilePath, validationResult.OutputFilePath);
            var equationTransformManager = serviceProvider.GetService<EquationTransformManager>();
            try
            {
                equationTransformManager.CountWordsAndWrite(validationResult.IOType, validationResult.IOType).Wait();
                ExitSuccess("Done!");
            }
            catch (Exception e)
            {
                ExitWithError(e.Message);
            }
        }

        private static void CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            ExitWithError("Program cancelled");
        }

        private static void ExitSuccess(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
            Environment.Exit(SuccessCode);
        }

        private static void ExitWithError(string error)
        {
            Console.WriteLine(error);
            Console.ReadKey();
            Environment.Exit(ErrorCode);
        }
    }
}
