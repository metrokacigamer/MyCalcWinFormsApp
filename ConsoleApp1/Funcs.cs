using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class Funcs
    {
        public static string ComputeExpression2(string str)// es varianti momivida azrad ro davweqi da ashkarad gacilebit sjobs :D
        {
            var separatorChars = new char[]
            {
                '+',
                '-',
                '*',
                '/'
            };
            var operands = str.Split(separatorChars)
                                .ToList();
            var operators = str.Where(x => IsOperator(x))
                                .ToList();
            var posOfMultAndDiv = operators.Select((x, i) => new { Character = x, Index = i })
                                            .Where(x => x.Character == '*' || x.Character == '/')
                                            .Select(x => x.Index)
                                            .ToList();

            foreach (var op in posOfMultAndDiv)
            {
                var result = 0.0;
                var firstOperandIndex = (operands[op] != string.Empty) ? op : op - 1;
                var secondOperandIndex = op + 1;

                if (operators[op] == '*')
                {
                    result = double.Parse(operands[firstOperandIndex]) * double.Parse(operands[secondOperandIndex]);
                }
                else
                {
                    result = double.Parse(operands[firstOperandIndex]) / double.Parse(operands[secondOperandIndex]);
                }

                operands[firstOperandIndex] = result.ToString();
                operands[secondOperandIndex] = string.Empty;
                operators[op] = default;
            }

            operands.RemoveAll(x => x.Equals(string.Empty));
            operators.RemoveAll(x => x.Equals(default));
            var posOfPlusAndMinus = operators.Select((x, i) => new { Character = x, Index = i })
                                            .Where(x => x.Character == '+' || x.Character == '-')
                                            .Select(x => x.Index)
                                            .ToList();
            foreach (var op in posOfPlusAndMinus)
            {
                var result = 0.0;
                var firstOperandIndex = (operands[op] != string.Empty) ? op : op - 1;
                var secondOperandIndex = op + 1;

                if (operators[op] == '+')
                {
                    result = double.Parse(operands[firstOperandIndex]) + double.Parse(operands[secondOperandIndex]);
                }
                else
                {
                    result = double.Parse(operands[firstOperandIndex]) - double.Parse(operands[secondOperandIndex]);
                }

                operands[firstOperandIndex] = result.ToString();
                operands[secondOperandIndex] = string.Empty;
                operators[op] = default;
            }

            operands.RemoveAll(x => x.Equals(string.Empty));
            operators.RemoveAll(x => x.Equals(default));

            var res = operands[0];
            var lastResult = StringToReturn(res);
            return lastResult;
        }

        private static string StringToReturn(string res)
        {
            var index = 0;
            if (LastOperandContainsDecimal(res))
            {
                index = res.Select((x, i) => new { Character = x, Index = i })
                                    .LastOrDefault(x => x.Character == '.')
                                    .Index;
            }
            if ((index == default && res.Length > 15) || index > 15)
            {
                return $"{res[0]}.{res[1]}{res[2]}{res[3]}e+{index}";
            }
            else if (res.Length - index + 1 > 12)
                return res.Substring(0, index + 12);
            else
                return res;
        }

        public static bool IsOperator(char el)
        {
            return el == '+' || el == '-' || el == '*' || el == '/';
        }

        public static bool LastOperandContainsDecimal(string expr)
        {
            if (!expr.Contains('.'))
                return false;
            var anonVar = expr.Select((x, i) => new { Character = x, Index = i })
                            .LastOrDefault(x => IsOperator(x.Character));
            var index = (anonVar == default) ? 0 : anonVar.Index;
            var lastOperand = expr.Substring(index);
            if (lastOperand.Contains('.'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string ComputeExpression(string str)
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


        public static List<int> PosOfMinPlus(StringBuilder str)
        {
            var list = str.ToString()
                .Select((x, i) => new { Character = x, Index = i })
                .Where(x => x.Character == '+' || x.Character == '-')
                .Select(x => x.Index)
                .ToList();
            return list;
        }

        public static List<int> PosOfMultDiv(StringBuilder str)
        {
            var list = str.ToString()
                .Select((x, i) => new { Character = x, Index = i })
                .Where(x => x.Character == '*' || x.Character == '/')
                .Select(x => x.Index)
                .ToList();
            return list;
        }

        public static double NumberBeforeOperator(StringBuilder str, int opIndex)
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

        public static double NumberAfterOperator(StringBuilder str, int opIndex)
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


        public static void ChangeOpWithResult(ref StringBuilder str, double result, int opIndex)
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

        public static string WhatToAdd(string text)
        {
            if (Funcs.LastOperandContainsDecimal(text))
            {
                return string.Empty;
            }
            else if (text == string.Empty || Funcs.IsOperator(text.Last()))
            {
                return "0.";
            }
            else
            {
                return ".";
            }
        }
    }
}
