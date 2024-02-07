using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Interfaces;
using ConsoleApp1.Attributes;



namespace ConsoleApp1.Functions_Versions
{
    public class FunctionsOld : IComputable
    {

        public (List<char>, List<string>) ComputeAdditionsAndSubtractions(List<char> operators, List<string> operands)
        {
            throw new NotImplementedException();
        }

        public (List<char>, List<string>) ComputeMultiplicationsAndDivisions(List<int> posOfMultAndDiv, List<char> operators, List<string> operands)
        {
            throw new NotImplementedException();
        }


        [Version("1.0", Description = "this is an initial method for computing the expression passed" +
                                " by buttonEquals eventhandler," +
                                " due to its poor design and performance its been left behind", IsInUse = false)]
        public string ComputeOperators(string str)
        {
            var tempStr = new StringBuilder(str);
            var MultDivides = PosOfMultDiv(tempStr);
            foreach (var op in MultDivides)
            {
                var operand1 = NumberBeforeOperator(tempStr, op);
                var operand2 = NumberAfterOperator(tempStr, op);
                if (tempStr[op] == '*')
                {
                    var res = operand1 * operand2;
                    ChangeOpWithResult(ref tempStr, res, op);
                }
                else
                {
                    var res = operand1 / operand2;
                    ChangeOpWithResult(ref tempStr, res, op);
                }
            }
            var MinPluses = PosOfMinPlus(tempStr);
            foreach (var op in MinPluses)
            {
                var operand1 = NumberBeforeOperator(tempStr, op);
                var operand2 = NumberAfterOperator(tempStr, op);
                if (tempStr[op] == '+')
                {
                    var res = operand1 + operand2;
                    ChangeOpWithResult(ref tempStr, res, op);
                }
                else
                {
                    var res = operand1 - operand2;
                    ChangeOpWithResult(ref tempStr, res, op);
                }
            }
            return double.Parse(tempStr.ToString()).ToString();
        }

        [Version("2.1", Description = "these methods for simple arithmetics were added in v2.1," +
                        " the intention is to stop parsing strings into doubles for computation" +
                        " sometime into the future," +
                        " Maybe I will write a code that does simple math operations" +
                        " on strings without casting them into doubles to increase accuracy," +
                        " but that is too much of a headache for the time being")]
        public string Subtraction(string v1, string v2)
        {
            var result = (double.Parse(v1) - double.Parse(v2)).ToString();
            return result;
        }

        public string Addition(string v1, string v2)
        {
            var result = (double.Parse(v1) + double.Parse(v2)).ToString();
            return result;
        }

        public string Division(string v1, string v2)
        {
            var result = (double.Parse(v1) / double.Parse(v2)).ToString();
            return result;
        }

        public string Multiplication(string v1, string v2)
        {
            var result = (double.Parse(v1) * double.Parse(v2)).ToString();
            return result;
        }

        [Version("1.0", Description = "could be of use in the future, but is too generic," +
                            " right now there are better alternatives depending on the situation", IsInUse = false)]
        public List<int> PosOfMinPlus(StringBuilder str)
        {
            var list = str.ToString()
                .Select((x, i) => new { Character = x, Index = i })
                .Where(x => x.Character == '+' || x.Character == '-')
                .Select(x => x.Index)
                .ToList();
            return list;
        }

        [Version("1.0", Description = "could be of use in the future, but is too generic," +
                            " right now there are better alternatives depending on the situation", IsInUse = false)]
        public List<int> PosOfMultDiv(StringBuilder str)
        {
            var list = str.ToString()
                .Select((x, i) => new { Character = x, Index = i })
                .Where(x => x.Character == '*' || x.Character == '/')
                .Select(x => x.Index)
                .ToList();
            return list;
        }

        [Version("1.0", Description = "these methods is not adviced to be used due to their poor design", IsInUse = false)]
        public double NumberBeforeOperator(StringBuilder str, int opIndex)
        {
            var temp = new StringBuilder();
            var i = 1;
            while (true)
            {
                if (opIndex < i)
                    break;
                else if (str[opIndex - i] == '+' || str[opIndex - i] == '-' || str[opIndex - i] == '/' || str[opIndex - i] == '*')
                    break;
                temp.Append(str[opIndex - i]);
                ++i;
            }
            var result = temp.ToString()
                .Reverse()
                .ToArray();
            return double.Parse(new string(result));
        }

        [Version("1.0", IsInUse = false)]
        public double NumberAfterOperator(StringBuilder str, int opIndex)
        {
            var temp = new StringBuilder();
            var i = 1;
            while (true)
            {
                if (opIndex + i == str.Length)
                    break;
                else if (str[opIndex + i] == '+' || str[opIndex + i] == '-' || str[opIndex + i] == '/' || str[opIndex + i] == '*')
                    break;

                temp.Append(str[opIndex + i]);
                ++i;
            }
            var result = temp.ToString();
            return double.Parse(result);
        }

        [Version("1.0", IsInUse = false)]
        public void ChangeOpWithResult(ref StringBuilder str, double result, int opIndex)
        {
            var i = 1;
            var j = 1;

            while (true)
            {
                if (opIndex + i == str.Length)
                    break;
                else if (str[opIndex + i] == '+' || str[opIndex + i] == '-' || str[opIndex + i] == '/' || str[opIndex + i] == '*')
                    break;
                ++i;
            }

            var endingIndex = opIndex + --i;

            while (true)
            {
                if (opIndex < j)
                    break;
                else if (str[opIndex - j] == '+' || str[opIndex - j] == '-' || str[opIndex - j] == '/' || str[opIndex - j] == '*')
                    break;
                ++j;
            }
            var startingIndex = opIndex - --j;


            var newStr = str.ToString()
                            .Remove(startingIndex, endingIndex - startingIndex + 1);

            var stringResult = result.ToString();

            newStr = newStr.Insert(startingIndex, stringResult);

            var count = endingIndex + 1 - startingIndex - stringResult.Length;

            var newStr2 = new string(' ', count);

            newStr = newStr.Insert(startingIndex + stringResult.Length, newStr2);

            str = new StringBuilder(newStr);
        }

    }
}
