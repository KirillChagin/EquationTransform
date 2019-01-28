using EquationTransform.Infrastructure;

namespace EquationTransform.CommandLineHelper
{
    internal class CommandLineResult
    {
        public IOType IOType { get; set; }

        public string InputFilePath { get; set; }

        public string OutputFilePath { get; set; }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
    }
}
