﻿using EquationTransform.IO.Contract;
using System.IO;
using System.Threading.Tasks;

namespace EquationTransform.IO.File
{
    /// <summary>
    /// Implementation of reading from a file
    /// </summary>
    public class EquationFileReader : IEquationReader
    {
        private bool _idDisposed;

        private FileStream _fileStream;
        private StreamReader _streamReader;

        /// <summary>
        /// Indicates that all lines in the file are read
        /// </summary>
        public bool ReadingCompleted { get; private set; }

        public EquationFileReader(string filePath)
        {
            _fileStream = System.IO.File.OpenRead(filePath);
            _streamReader = new StreamReader(_fileStream);
        }

        /// <summary>
        /// Read next line from a file
        /// </summary>
        /// <returns>next equation string</returns>
        public string ReadNextEquation()
        {
            if (_streamReader.EndOfStream)
            {
                ReadingCompleted = true;
                return null;
            }
            return _streamReader.ReadLine();
        }

        /// <summary>
        /// Read next line from a file
        /// </summary>
        /// <returns>next equation string</returns>
        public async Task<string> ReadNextEquationAsync()
        {
            if (_streamReader.EndOfStream)
            {
                ReadingCompleted = true;
                return null;
            }
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
