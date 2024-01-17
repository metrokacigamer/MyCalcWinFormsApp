using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Funcs
    {
        //v2.1
        public string ComputeOperators(string str) // es varianti momivida azrad ro davweqi da ashkarad gacilebit sjobs :D
        {
            if (str == string.Empty)
            {
                return "0";
            }

            var separatorChars = new char[]
            {
                '+',
                '-',
                '*',
                '/'
            };
            var operands = str.Split(separatorChars)
                                .ToList();
            operands.RemoveAll(x => x == string.Empty);
            var operatorsIndexes = str.Select((x, i) => new { Character = x, Index = i })
                                        .Where(x => IsOperator(x.Character))
                                        .ToList();
            if (!operatorsIndexes.Any())
            {
                return str;
            }
            if (operatorsIndexes.Count >= operatorsIndexes.Count)
            {
                if (operatorsIndexes[0].Index == 0)
                {
                    operatorsIndexes.RemoveAt(0);
                    operands[0] = new string(operands[0].Prepend('-')
                                                        .ToArray());
                }
                var indexesToRemove = new List<int>();
                var operandsCount = 0;
                for (var x = 0; x < operatorsIndexes.Count;)// in minuses)
                {
                    if (IsOperator(str[operatorsIndexes[x].Index - 1]))
                    {
                        operands[operandsCount + 1] = new string(operands[operandsCount + 1].Prepend('-')
                                    .ToArray());
                        //indexesToRemove.Add(operatorsIndexes[x].Index);
                        operatorsIndexes.RemoveAt(x);
                        ++operandsCount;
                    }
                    else
                        ++x;
                }
            }
            var operators = operatorsIndexes.Select(x => x.Character)
                                            .ToList();
            var posOfMultAndDiv = operators.Select((x, i) => new { Character = x, Index = i })
                                            .Where(x => x.Character == '*' || x.Character == '/')
                                            .Select(x => x.Index)
                                            .ToList();

            foreach (var op in posOfMultAndDiv)
            {
                var result = 0.0;
                var firstOperandIndex = op;
                var secondOperandIndex = op + 1;

                if (operators[op] == '*')
                {
                    result = double.Parse(operands[firstOperandIndex]) * double.Parse(operands[secondOperandIndex]);
                }
                else
                {
                    result = double.Parse(operands[firstOperandIndex]) / double.Parse(operands[secondOperandIndex]);
                }
                var stringRes = result.ToString();
                operands[firstOperandIndex] = string.Empty;
                operands[secondOperandIndex] = stringRes;
                operators[op] = default;
            }

            operands.RemoveAll(x => x.Equals(string.Empty));
            operators.RemoveAll(x => x.Equals(default));

            for (int op = 0; op < operators.Count; ++op)
            {
                var result = 0.0;
                var firstOperandIndex = op;
                var secondOperandIndex = op + 1;

                if (operators[op] == '+')
                {
                    result = double.Parse(operands[firstOperandIndex]) + double.Parse(operands[secondOperandIndex]);
                }
                else
                {
                    result = double.Parse(operands[firstOperandIndex]) - double.Parse(operands[secondOperandIndex]);
                }

                var stringRes = result.ToString();
                operands[firstOperandIndex] = string.Empty;
                operands[secondOperandIndex] = stringRes;
            }

            operands.RemoveAll(x => x.Equals(string.Empty));

            var res = operands[0];
            var lastResult = StringToReturn(res);
            return lastResult;
        }

        public string ComputeExpression2(string str)
        {
            var endIndex = str.LastIndexOf(')');
            if (endIndex == -1)
            {
                return ComputeOperators(str);
            }
            else if ((endIndex == str.Length - 1) && (FindAMatchingParenthesis(str.Substring(0, str.Length - 1)) == 0))
            {
                return ComputeExpression2(str.Substring(1, str.Length - 2));
            }
            var expressionList = ExpressionOpList(str).Item1;
            var operatorList = ExpressionOpList(str).Item2;

            for (var i = 0; i < expressionList.Count; ++i)
            {
                expressionList[i] = ComputeExpression2(expressionList[i]);
            }
            var joinedExpressionList = new string(string.Empty);

            for (var i = 0; i < expressionList.Count - 1; ++i)
            {
                expressionList[i] += operatorList[i];
                joinedExpressionList += expressionList[i];
            }
            joinedExpressionList += expressionList.Last();
            var lastResult = ComputeOperators(joinedExpressionList);
            return lastResult;
        }

        public void SpecialCheckForNegatives(string str)
        {

        }

        public (List<string>, List<char>) ExpressionOpList(string str)
        {
            var result = new List<string>();
            var ops = new List<char>();

            var tempStr = str;
            var endIndex = 0;
            var startIndex = 0;
            while (!tempStr.Equals(string.Empty))
            {
                endIndex = tempStr.LastIndexOf(')');
                startIndex = FindAMatchingParenthesis(tempStr.Substring(0, Math.Abs(endIndex)));
                if (tempStr.Substring(endIndex + 1) != string.Empty)
                {
                    result.Add(tempStr.Substring(endIndex + 1));
                    tempStr = tempStr.Substring(0, endIndex + 1);
                }
                else
                {
                    result.Add(tempStr.Substring(startIndex, endIndex + 1 - startIndex));
                    tempStr = tempStr.Substring(0, startIndex);
                }
            }
            result.Reverse();
            for (var i = 0; i < result.Count; ++i)
            {
                if (IsOperator(result[i].First()))
                {
                    ops.Add(result[i].First());
                    result[i] = result[i].Remove(0, 1);
                    if (result[i] == string.Empty)
                    {
                        result.Remove("");
                    }
                }

                if (IsOperator(result[i].Last()))
                {
                    ops.Add(result[i].Last());
                    result[i] = result[i].Remove(result[i].Length - 1, 1);
                    if (result[i] == string.Empty)
                    {
                        result.Remove("");
                    }
                }
            }
            return (result, ops);
        }

        public string StringToReturn(string res)
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

        public string WhatToAdd(string text)
        {
            if (LastOperandContainsDecimal(text))
            {
                return string.Empty;
            }
            else if (text == string.Empty || IsOperator(text.Last()))
            {
                return "0.";
            }
            else
            {
                return ".";
            }
        }

        public bool IsOperator(char el)
        {
            return el == '+' || el == '-' || el == '*' || el == '/';
        }

        public bool LastOperandContainsDecimal(string expr)
        {
            if (!expr.Contains('.'))
            {
                return false;
            }
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

        //v1.0

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

        public string AddParenthesis_1(string text)
        {
            if (text == string.Empty || IsOperator(text.Last()) || text.Last() == '(')
            {
                return "(";
            }
            else
            {
                return "*(";
            }
        }

        public string AddParenthesis_2(string text)
        {
            if (HasAMatchingParenthesis_1(text, out _) || !IsOperator(text.Last()))
            {
                return ")";
            }
            else
            {
                return string.Empty;
            }
        }

        public bool HasAMatchingParenthesis_1(string text, out int index)
        {
            index = default;
            if (text.Count(x => x == '(') > text.Count(x => x == ')'))
            {
                index = FindAMatchingParenthesis(text);
                return true;
            }
            else
            {
                return false;
            }
        }

        public int FindAMatchingParenthesis(string text)
        {
            if (!text.Contains('('))
            {
                return 0;
            }

            var i = 0;
            for (var count = 1; count != 0; ++i)
            {
                if (text[text.Length - i - 1] == '(')
                {
                    --count;
                }
                else if (text[text.Length - i - 1] == ')')
                {
                    ++count;
                }
            }
            return text.Length - i;
        }
    }
}
