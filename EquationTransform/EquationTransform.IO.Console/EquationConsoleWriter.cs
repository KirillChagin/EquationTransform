using EquationTransform.IO.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EquationTransform.IO.Console
{
    public class EquationConsoleWriter : IEquationWriter
    {
        private bool _idDisposed;

        public void WriteNextEquation(string outputString)
        {
            System.Console.WriteLine(outputString);
        }

        public Task WriteNextEquationAsync(string outputString)
        {
            WriteNextEquation(outputString);
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            if (!_idDisposed)
            {
                _idDisposed = true;
            }
        }
    }
}
