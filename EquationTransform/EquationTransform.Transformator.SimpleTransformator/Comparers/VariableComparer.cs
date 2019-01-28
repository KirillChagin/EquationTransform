using System;
using System.Collections.Generic;
using System.Text;

namespace EquationTransform.Transformator.SimpleTransformator.Comparers
{
    internal class VariableComparer : IComparer<Variable>
    {
        public int Compare(Variable x, Variable y)
        {
            if (x.Power > y.Power)
            {
                return 1;
            }
            else if (x.Power < y.Power)
            {
                return -1;
            }
            else
            {
                return x.Name - y.Name;
            }

        }
    }
}
