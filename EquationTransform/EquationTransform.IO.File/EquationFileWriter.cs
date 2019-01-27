using EquationTransform.IO.Contract;
using System.IO;
using System.Threading.Tasks;

namespace EquationTransform.IO.File
{
    public class EquationFileWriter : IEquationWriter
    {
        private bool _idDisposed;

        private FileStream _fileStream;
        private StreamWriter _streamWriter;

        public EquationFileWriter(string filePath)
        {
            _fileStream = System.IO.File.OpenWrite(filePath);
            _streamWriter = new StreamWriter(_fileStream);
        }

        public void WriteNextEquation(string outputString)
        {
            _streamWriter.WriteLine(outputString);
        }

        public async Task WriteNextEquationAsync(string outputString)
        {
            await _streamWriter.WriteLineAsync(outputString);
        }

        public void Dispose()
        {
            if (!_idDisposed)
            {
                try
                {
                    if (_fileStream != null)
                    {
                        _streamWriter.Close();
                        _fileStream.Close();
                    }
                }
                finally
                {
                    _streamWriter = null;
                    _fileStream = null;
                    _idDisposed = true;
                }
            }
        }
    }
}
