using EquationTransform.IO.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquationTransform.IO.File
{
    public class EquationFileIO : IEquationReader, IEquationWriter
    {
        public string Read()
        {
            throw new NotImplementedException();
        }

        public string ReadAll()
        {
            throw new NotImplementedException();
        }

        public Task<string> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public void Write(string outputString)
        {
            throw new NotImplementedException();
        }

        public void WriteAll(List<string> outputStrings)
        {
            throw new NotImplementedException();
        }

        public Task WriteAllAsync(List<string> outputStrings)
        {
            throw new NotImplementedException();
        }

        public Task WriteAsync(string outputString)
        {
            throw new NotImplementedException();
        }
    }
}
