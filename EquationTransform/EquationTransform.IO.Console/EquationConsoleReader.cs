using EquationTransform.IO.Contract;
using System;
using System.Threading.Tasks;

namespace EquationTransform.IO.Console
{
    public class EquationConsoleReader : IEquationReader
    {
        private bool _idDisposed;

        public bool ReadingCompleted { get; private set; }

        public EquationConsoleReader()
        {
            System.Console.CancelKeyPress += CancelKeyPress;
        }

        public string ReadNextEquation()
        {
            return System.Console.ReadLine();
        }

        public Task<string> ReadNextEquationAsync()
        {
            return Task.FromResult(ReadNextEquation());
        }

        private void CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            ReadingCompleted = true;
        }

        public void Dispose()
        {
            if (!_idDisposed)
            {
                System.Console.CancelKeyPress -= CancelKeyPress;
                _idDisposed = true;
            }
        }
    }
}
