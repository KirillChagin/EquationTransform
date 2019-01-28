using EquationTransform.Transformator.Contract.Exceptions;
using EquationTransform.Transformator.SimpleTransformator.Comparers;
using System.Collections.Generic;
using System.Text;

namespace EquationTransform.Transformator.SimpleTransformator
{
    internal class Summand
    {
        public string OriginalString { get; private set; }

        public string CanonicalString { get; private set; }

        public List<Variable> CanonicalVariables { get; private set; }

        public Summand(string summandString)
        {
            OriginalString = summandString;
            ConvertToCanonical();
        }

        public int GetMaxPower()
        {
            var maxPower = int.MinValue;
            foreach (var variable in CanonicalVariables)
            {
                if (variable.Power > maxPower)
                {
                    maxPower = variable.Power;
                }
            }
            return maxPower;
        }

        //TODO: need simplification
        private void ConvertToCanonical()
        {
            //variable names and powers dictionary
            var variables = new List<Variable>();
            Variable currentVariable = null;
            for (var i = 0; i < OriginalString.Length; i++)
            {
                var currentChar = OriginalString[i];
                if (char.IsLetter(currentChar))
                {
                    currentVariable = new Variable() { Name = OriginalString[i], Power = 1 };
                    variables.Add(currentVariable);
                }
                else if (currentChar == '^')
                {
                    if (currentVariable == null)
                    {
                        throw new IncorrectEquationFormatException("No variable before power operator");
                    }
                    i++;
                    if (i >= OriginalString.Length)
                    {
                        throw new IncorrectEquationFormatException("Variable has power sign but no power number");
                    }
                    var powerString = new StringBuilder();

                    int sign = 1;
                    if (OriginalString[i] == '-')
                    {
                        sign = -1;
                        i++;
                    }
                    while (i < OriginalString.Length && !char.IsLetter(OriginalString[i]))
                    {
                        powerString.Append(OriginalString[i]);
                        i++;
                    }
                    i--;
                    var parseResult = int.TryParse(powerString.ToString(), out int power);
                    if (!parseResult)
                    {
                        throw new IncorrectEquationFormatException(string.Format("Incorrect variable power: {0}", powerString));
                    }
                    currentVariable.Power = power * sign;
                }
            }
            var comparer = new VariableComparer();
            variables.Sort(comparer);

            CanonicalVariables = variables;
            CanonicalString = GenerateCanonicalString();
        }

        private string GenerateCanonicalString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var variable in CanonicalVariables)
            {
                if (CanonicalVariables.Count > 1 && variable.Power != 1)
                {
                    stringBuilder.Append("(");
                }
                stringBuilder.Append(variable.Name);
                if (variable.Power != 1)
                {
                    if (variable.Power < 0)
                    {
                        stringBuilder.AppendFormat("^({0})", variable.Power);
                    }
                    else
                    {
                        stringBuilder.AppendFormat("^{0}", variable.Power);
                    }
                }
                if (CanonicalVariables.Count > 1 && variable.Power != 1)
                {
                    stringBuilder.Append(")");
                }
            }

            return stringBuilder.ToString();
        }

        public override int GetHashCode()
        {
            return CanonicalString.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var summand = obj as Summand;
            if (summand == null)
            {
                return false;
            }
            else
            {
                return CanonicalString == summand.CanonicalString;
            }
        }
    }
}
