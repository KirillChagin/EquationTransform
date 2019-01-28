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
            System.Console.WriteLine("Please enter the equation in the following form:");
            System.Console.WriteLine("P1 + P2 + ... = ... + PN");
            System.Console.WriteLine("where P1..PN - summands, which look like: ");
            System.Console.WriteLine("ax^k ");
            System.Console.WriteLine("where a - floating point value; ");
            System.Console.WriteLine("k - integer value; ");
            System.Console.WriteLine("x - variable (each summand can have many variables).");
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
            System.Console.WriteLine("Press any key to exit...");
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
