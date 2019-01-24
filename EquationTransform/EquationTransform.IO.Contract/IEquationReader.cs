using System.Threading.Tasks;

namespace EquationTransform.IO.Contract
{
    public interface IEquationReader
    {
        string Read();

        string ReadAll();

        Task<string> ReadAsync();

        Task<string> ReadAllAsync();
    }
}
