using EquationTransform.IO.Contract;
using System.IO;
using System.Threading.Tasks;

namespace EquationTransform.IO.File
{
    public class EquationFileReader : IEquationReader
    {
        private bool _idDisposed;

        private FileStream _fileStream;
        private StreamReader _streamReader;

        public bool ReadingCompleted { get; private set; }

        public EquationFileReader(string filePath)
        {
            _fileStream = System.IO.File.OpenRead(filePath);
            _streamReader = new StreamReader(_fileStream);
        }

        public string ReadNextEquation()
        {
            return _streamReader.ReadLine();
        }

        public async Task<string> ReadNextEquationAsync()
        {
            return await _streamReader.ReadLineAsync();
        }

        public void Dispose()
        {
            if (!_idDisposed)
            {
                try
                {
                    if (_fileStream != null)
                    {
                        _streamReader.Close();
                        _fileStream.Close();
                    }
                }
                finally
                {
                    _streamReader = null;
                    _fileStream = null;
                    _idDisposed = true;
                }  
            }
        }
    }
}
