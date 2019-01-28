using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquationTransform.IO.Contract
{
    public interface IEquationWriter : IDisposable
    {
        /// <summary>
        /// Write equation string to the destination
        /// </summary>
        /// <param name="outputString">output string</param>
        void WriteNextEquation(string outputString);

        /// <summary>
        /// Write equation string to the destination
        /// </summary>
        /// <param name="outputString">output string</param>
        Task WriteNextEquationAsync(string outputString);
    }
}
