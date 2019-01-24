using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquationTransform.IO.Contract
{
    public interface IEquationWriter
    {
        void Write(string outputString);

        void WriteAll(List<string> outputStrings);

        Task WriteAsync(string outputString);

        Task WriteAllAsync(List<string> outputStrings);
    }
}
