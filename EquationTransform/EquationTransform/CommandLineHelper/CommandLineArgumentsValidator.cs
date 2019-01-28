using System;
using System.IO;

namespace EquationTransform.CommandLineHelper
{
    internal static class CommandLineArgumentsValidator
    {
        public static CommandLineResult GetCommandLineResult(string[] args)
        {
            var result = new CommandLineResult() { IOType = Infrastructure.IOType.Console, Success = false };

            if (args.Length == 0)
            {
                result.Success = true;
                return result;
            }

            if (args.Length > 1)
            {
                result.ErrorMessage = "Incorrect number of arguments passed. Please use no arguments for console mode, or file path for file mode.";
                return result;
            }

            if (!File.Exists(args[0]))
            {
                result.ErrorMessage = "Specified file not exists.";
                return result;
            }

            var outFilePath = "";
            if (!string.IsNullOrWhiteSpace(args[0]))
            {
                outFilePath = args[0] + ".out";
                try
                {
                    using (var file = File.Create(outFilePath)) { }
                    result.IOType = Infrastructure.IOType.File;
                    result.InputFilePath = args[0];
                    result.OutputFilePath = outFilePath;
                }
                catch(Exception e)
                {
                    result.Success = false;
                    result.ErrorMessage = e.Message;
                    return result;
                }
            }

            result.Success = true;
            return result;
        }
    }
}
