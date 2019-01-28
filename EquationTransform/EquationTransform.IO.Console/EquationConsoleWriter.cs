using EquationTransform.IO.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EquationTransform.IO.Console
{
    /// <summary>
    /// Implementation of writing to console
    /// </summary>
    public class EquationConsoleWriter : IEquationWriter
    {
        private bool _idDisposed;

        /// <summary>
        /// Write equation to the console
        /// </summary>
        /// <param name="outputString">output string</param>
        public void WriteNextEquation(string outputString)
        {
            System.Console.WriteLine(outputString);
        }

        /// <summary>
        /// Write equation to the console
        /// </summary>
        /// <param name="outputString">output string</param>
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
