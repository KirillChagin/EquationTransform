using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquationTransform.IO.Contract
{
    public interface IEquationWriter : IDisposable
    {
        void WriteNextEquation(string outputString);

        Task WriteNextEquationAsync(string outputString);
    }
}
