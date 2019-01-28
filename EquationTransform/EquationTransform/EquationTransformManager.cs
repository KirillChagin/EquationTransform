using EquationTransform.Infrastructure;
using EquationTransform.IO.Contract;
using EquationTransform.Transformator.Contract;
using EquationTransform.Transformator.Contract.Exceptions;
using System;
using System.Threading.Tasks;

namespace EquationTransform
{
    public class EquationTransformManager
    {
        private readonly Func<IOType, IEquationReader> _readService;
        private readonly Func<IOType, IEquationWriter> _writeService;
        private readonly IEquationTransformator _transformator;

        public EquationTransformManager(Func<IOType, IEquationReader> readService,
            Func<IOType, IEquationWriter> writeService,
            IEquationTransformator transformator)
        {
            _readService = readService;
            _writeService = writeService;
            _transformator = transformator;
        }

        public async Task CountWordsAndWrite(IOType readType, IOType writeType)
        {
            using (var reader = _readService(readType))
            {
                using (var writer = _writeService(writeType))
                {
                    while (!reader.ReadingCompleted)
                    {
                        var equation = await reader.ReadNextEquationAsync();
                        if (!string.IsNullOrEmpty(equation))
                        {
                            try
                            {
                                var canonicalEquation = _transformator.TransformToCanonical(equation);
                                await writer.WriteNextEquationAsync(canonicalEquation);
                            }
                            catch (IncorrectEquationFormatException e)
                            {
                                await writer.WriteNextEquationAsync(e.Message);
                            }
                        }
                    }
                }
            }
        }
    }
}
