using EquationTransform.Transformator.Contract;
using EquationTransform.Transformator.Contract.Exceptions;
using EquationTransform.Transformator.SimpleTransformator.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquationTransform.Transformator.SimpleTransformator
{
    public class SimpleEquationTransformator : IEquationTransformator
    {
        //no other operators by assumption of the task
        private readonly List<string> _operators = new List<string> { "+", "-" };
        private readonly List<char> _specialSymbols = new List<char> { '(', ')', '+', '-' }; 

        private Dictionary<Summand, double> _summands = new Dictionary<Summand, double>();

        /// <summary>
        /// Transforms equation to the canonical form
        /// </summary>
        /// <param name="equation">input equation</param>
        /// <returns>Equation in the canonical form</returns>
        public string TransformToCanonical(string equation)
        {
            if (equation == null)
            {
                throw new ArgumentNullException("Equation can't be null");
            }

            if (string.IsNullOrWhiteSpace(equation))
            {
                throw new ArgumentException("Equation can't be an empty string");
            }

            var parts = equation.Split('=');
            if (parts.Length != 2)
            {
                throw new IncorrectEquationFormatException(string.Format("Equation {0} has no '=' sign", equation));
            }

            try
            {
                ParseSummands(parts[0], 1);
                ParseSummands(parts[1], -1);

                return CombineResult();
            }
            catch(Exception e)
            {
                throw new IncorrectEquationFormatException("Incorrect format", e);
            }
        }

        //TODO: split method
        private void ParseSummands(string equation, int sign)
        {
            List<Tuple<string, int>> parsedTokens = new List<Tuple<string, int>>();

            var tokens = GetTokens(equation);
            var currentSign = sign;
            int tokenIndex = 0;

            while (tokenIndex < tokens.Count)
            {
                var token = tokens[tokenIndex];
                if (token == "(")
                {
                    var subExpression = GetSubExpression(tokens, ref tokenIndex);
                    ParseSummands(subExpression, currentSign);
                    currentSign = sign;
                    continue;
                }
                if (token == ")")
                {
                    throw new IncorrectEquationFormatException("Mismatched parentheses in equation");
                }
                if (_operators.Contains(token))
                {
                    if (token == "-")
                    {
                        currentSign *= -1;
                    }
                    else
                    {
                        currentSign = sign;
                    }
                }
                else
                {
                    parsedTokens.Add(new Tuple<string, int>(token, currentSign));
                }

                tokenIndex += 1;
            }

            foreach(var parsedToken in parsedTokens)
            {
                AddToken(parsedToken.Item1, parsedToken.Item2);
            }
        }

        private List<string> GetTokens(string equation)
        {
            List<string> tokens = new List<string>();
            StringBuilder sb = new StringBuilder();

            var trimmedEquation = equation.Replace(" ", string.Empty);
            //For the cases (x)(y)
            trimmedEquation = trimmedEquation.Replace(")(", string.Empty);
            for (var i = 0; i < trimmedEquation.Length; i++)
            {
                var c = trimmedEquation[i];
                if (c == '^')
                {
                    sb.Append(c);
                    if (i + 1 >= trimmedEquation.Length)
                    {
                        throw new IncorrectEquationFormatException("Incorrect power");
                    }
                    //For the cases x^-2
                    else if (trimmedEquation[i + 1] == '-')
                    {
                        sb.Append(trimmedEquation[i + 1]);
                        i++;
                    }
                    //For the cases x^(-2)
                    else if (trimmedEquation[i+1] == '(')
                    {
                        i+=2;
                        while (i < trimmedEquation.Length && trimmedEquation[i] != ')')
                        {
                            sb.Append(trimmedEquation[i]);
                            i++;
                        }
                    }
                }
                else if (c == '(' && i > 0 && char.IsDigit(trimmedEquation[i - 1]))
                {
                    //For the cases 3.5(x)
                    i++;
                    while (i < trimmedEquation.Length && trimmedEquation[i] != ')')
                    {
                        sb.Append(trimmedEquation[i]);
                        i++;
                    }
                }
                else if (_specialSymbols.Contains(c))
                {
                    if (sb.Length > 0)
                    {
                        tokens.Add(sb.ToString());
                        sb.Length = 0;
                    }
                    tokens.Add(c.ToString());
                }
                else
                {
                    sb.Append(c);
                }
            }

            if (sb.Length > 0)
            {
                tokens.Add(sb.ToString());
            }

            return tokens;
        }

        private string GetSubExpression(List<string> tokens, ref int index)
        {
            StringBuilder subExpression = new StringBuilder();
            int parenthesesLevel = 1;
            index += 1;
            while (index < tokens.Count && parenthesesLevel > 0)
            {
                string token = tokens[index];
                if (tokens[index] == "(")
                {
                    parenthesesLevel += 1;
                }

                if (tokens[index] == ")")
                {
                    parenthesesLevel -= 1;
                }

                if (parenthesesLevel > 0)
                {
                    subExpression.Append(token);
                }

                index += 1;
            }

            if (parenthesesLevel > 0)
            {
                throw new IncorrectEquationFormatException("Mismatched parentheses in equation");
            }
            return subExpression.ToString();
        }

        private void AddToken(string token, int sign)
        {
            int index = 0;
            while (index < token.Length && !char.IsLetter(token[index]))
            {
                ++index;
            }
            var stringValue = token.Substring(0, index);
            var variables = token.Substring(index, token.Length - index);

            double value;
            if (index > 0)
            {
                var result = double.TryParse(stringValue, out value);
                if (!result)
                {
                    throw new IncorrectEquationFormatException(string.Format("Can't parse double {0}", stringValue));
                }
            }
            else
            {
                value = 1;
            }

            var summand = new Summand(variables);

            if (_summands.ContainsKey(summand))
            {
                var newValue = _summands[summand] + value * sign;
                _summands[summand] = newValue;
            }
            else
            {
                _summands.Add(summand, value * sign);
            }
        }

        private string CombineResult()
        {
            var sortedKeys = GetSortedSummandsKeys();
            var resultSignInvertor = 1;

            //if the first summand is negative - invert all values
            if (_summands[sortedKeys.First()] < 0)
            {
                resultSignInvertor = -1;
            }
            StringBuilder sb = new StringBuilder();
            foreach(var key in sortedKeys)
            {
                var summandValue = _summands[key];
                if (summandValue == 0)
                {
                    continue;
                }

                var invertedSummandValue = summandValue * resultSignInvertor;
                var sign = "";
                //First element always positive and has no sign before
                if (sb.Length > 0)
                {
                    sign = invertedSummandValue > 0 ? " + " : " - ";
                }
                sb.Append(sign);
                if (Math.Abs(invertedSummandValue) != 1)
                {
                    sb.Append(Math.Abs(invertedSummandValue));
                }
                sb.Append(key.CanonicalString);
            }
            if (sb.Length == 0)
            {
                sb.Append("0");
            }
            sb.Append(" = 0");
            return sb.ToString();
        }

        private List<Summand> GetSortedSummandsKeys()
        {
            var keys = _summands.Keys.ToList();
            var comparer = new SummandsComparer(StringComparer.CurrentCulture);
            keys.Sort(comparer);
            return keys;
        }
    }
}
