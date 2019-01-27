using System;

namespace EquationTransform.Transformator.Contract.Exceptions
{
    public class IncorrectEquationFormatException : Exception
    {
        public IncorrectEquationFormatException()
        {
        }

        public IncorrectEquationFormatException(string message)
            : base(message)
        {
        }

        public IncorrectEquationFormatException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
