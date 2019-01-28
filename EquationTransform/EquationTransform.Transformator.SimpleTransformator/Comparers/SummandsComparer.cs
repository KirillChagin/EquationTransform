using System;
using System.Collections.Generic;
using System.Text;

namespace EquationTransform.Transformator.SimpleTransformator.Comparers
{
    internal class SummandsComparer : IComparer<Summand>
    {
        private readonly IComparer<string> _baseComparer;
        public SummandsComparer(IComparer<string> baseComparer)
        {
            _baseComparer = baseComparer;
        }

        public int Compare(Summand x, Summand y)
        {
            if (_baseComparer.Compare(x.CanonicalString, "") == 0)
                return 1;
            if (_baseComparer.Compare(y.CanonicalString, "") == 0)
                return -1;

            var xMaxPower = x.GetMaxPower();
            var yMaxPower = y.GetMaxPower();
            if (xMaxPower == yMaxPower)
            {
                return _baseComparer.Compare(x.CanonicalString, y.CanonicalString);
            }
            else
            {
                return yMaxPower - xMaxPower;
            }
        }
    }
}
