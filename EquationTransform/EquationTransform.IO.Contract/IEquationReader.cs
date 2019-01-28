using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquationTransform.IO.Contract
{
    public interface IEquationReader : IDisposable
    {
        /// <summary>
        /// Get the next equation string from the input source
        /// </summary>
        /// <returns>Equation string</returns>
        string ReadNextEquation();

        /// <summary>
        /// Get the next equation string from the input source
        /// </summary>
        /// <returns>Equation string</returns>
        Task<string> ReadNextEquationAsync();

        /// <summary>
        /// True if all equations are read
        /// </summary>
        bool ReadingCompleted { get; }
    }
}
