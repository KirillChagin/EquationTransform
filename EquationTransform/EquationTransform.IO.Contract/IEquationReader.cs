using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquationTransform.IO.Contract
{
    public interface IEquationReader : IDisposable
    {
        string ReadNextEquation();

        Task<string> ReadNextEquationAsync();

        bool ReadingCompleted { get; }
    }
}
