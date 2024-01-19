using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Funcs
    {
        [Version("2.1", Description = "some methods are upgraded from v2.0", IsUpgradedFromPrevVersions = true)]
        public string ComputeOperators(string str) // es varianti momivida azrad ro davweqi da ashkarad gacilebit sjobs :D
        {
            if (str == string.Empty)
            {
                return "0";
            }
            else if (!str.Any(x => IsOperator(x)))
            {
                return str;
            }

            var operands = GenerateOperandsList(str);
            var operatorsIndexes = GenerateOperatorsAndIndexesList(str);

            SpecialCheckAndActForNegatives(ref str, ref operands, ref operatorsIndexes);

            var operators = operatorsIndexes.Select(x => x.character)
                                            .ToList();
            var posOfMultAndDiv = operators.Select((x, i) => new { Character = x, Index = i })
                                            .Where(x => x.Character == '*' || x.Character == '/')
                                            .Select(x => x.Index)
                                            .ToList();

            MultiplicationsAndDivisions(posOfMultAndDiv, ref operators, ref operands);
            AdditionsAndSubtractions(ref operators, ref operands);

            var res = operands[0];
            var lastResult = StringToReturn(res);
            return lastResult;
        }

        private void AdditionsAndSubtractions(ref List<char> operators, ref List<string> operands)
        {
            for (int op = 0; op < operators.Count; ++op)
            {
                var result = string.Empty;
                var firstOperandIndex = op;
                var secondOperandIndex = op + 1;

                if (operators[op] == '+')
                {
                    result = Addition(operands[firstOperandIndex], operands[secondOperandIndex]);
                }
                else
                {
                    result = Subtraction(operands[firstOperandIndex], operands[secondOperandIndex]);
                }
                operands[firstOperandIndex] = string.Empty;
                operands[secondOperandIndex] = result;
            }
            operands.RemoveAll(x => x.Equals(string.Empty));
        }

        private void MultiplicationsAndDivisions(List<int> posOfMultAndDiv, ref List<char> operators, ref List<string> operands)
        {
            foreach (var op in posOfMultAndDiv)
            {
                var result = string.Empty;
                var firstOperandIndex = op;
                var secondOperandIndex = op + 1;

                if (operators[op] == '*')
                {
                    result = Multiplication(operands[firstOperandIndex], operands[secondOperandIndex]);
                }
                else
                {
                    result = Division(operands[firstOperandIndex], operands[secondOperandIndex]);
                }
                operands[firstOperandIndex] = string.Empty;
                operands[secondOperandIndex] = result;
                operators[op] = default;
            }
            operands.RemoveAll(x => x.Equals(string.Empty));
            operators.RemoveAll(x => x.Equals(default));
        }

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

        public List<string> GenerateOperandsList(string str)
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
            operands.RemoveAll(x => x == string.Empty);
            return operands;
        }

        private List<(char character, int index)> GenerateOperatorsAndIndexesList(string str)
        {
            var result = str.Select((character, index) => (character, index))
                            .Where(x => IsOperator(x.character))
                            .ToList();
            return result;
        }

        public string ComputeExpressionV2_1(string str)
        {
            var endIndex = str.LastIndexOf(')');
            if (endIndex == -1)
            {
                return ComputeOperators(str);
            }
            else if ((endIndex == str.Length - 1) && (FindAMatchingParenthesis(str.Substring(0, str.Length - 1)) == 0))
            {
                return ComputeExpressionV2_1(str.Substring(1, str.Length - 2));
            }

            var callExpressionOpList = ExpressionOpList(str);
            var expressionList = callExpressionOpList.Item1;
            var operatorList = callExpressionOpList.Item2;

            for (var i = 0; i < expressionList.Count; ++i)
            {
                expressionList[i] = ComputeExpressionV2_1(expressionList[i]);
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

        public void SpecialCheckAndActForNegatives(ref string str, ref List<string> operands, ref List<(char character, int index)> operatorsIndexes)
        {
            if (operatorsIndexes.Count >= operatorsIndexes.Count)
            {
                var operandsCount = 0;
                if (operatorsIndexes[0].index == 0)
                {
                    operands[operandsCount] = new string(operands[operandsCount].Prepend('-')
                                                                                .ToArray());
                    operatorsIndexes.RemoveAt(0);
                }
                for (var x = 0; x < operatorsIndexes.Count;)
                {
                    if (IsOperator(str[operatorsIndexes[x].index - 1]))
                    {
                        operands[operandsCount + 1] = new string(operands[operandsCount + 1].Prepend('-')
                                    .ToArray());
                        operatorsIndexes.RemoveAt(x);
                        ++operandsCount;
                    }
                    else
                    {
                        ++x;
                    }
                }
            }
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

        public string DecideWhatToAdd(string text)
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

        [Version("1.0", Description = "below methods are no longer in use")]

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

        [Version("2.1", Description = "below methods are used only in Form1.cs")]
        public string DecideHowToAddParenthesis_1(string text)
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

        public string DecideHowToAddParenthesis_2(string text)
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

        [Version("2.1")]

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

        [Version("2.2")]
        public string DecideHowToAddTrigFunc(string text, string FuncName, out int indexToRemoveFrom)
        {
            if (text == string.Empty || text.Last() == '(')// "+|-123",
            {
                indexToRemoveFrom = text.Length;
                return $"{FuncName}(0)";
            }
            else if (IsOperator(text.Last()))
            {
                indexToRemoveFrom = text.Length;
                return $"{FuncName}(0)";
            }
            else if (text.Last() == ')' && !LastIsATrigonometricExpression(text, out _))
            {
                var index = FindAMatchingParenthesis(text.Substring(0, text.Length - 1));
                var expression = text.Substring(index, text.Length - index);
                if(LastOperandIsNegative(text, out var signIndex))
                {
                    expression = expression.Insert(0, "(-");
                    expression = expression.Insert(expression.Length - 1, ")");
                    index = signIndex;
                }
                indexToRemoveFrom = index;
                return $"{FuncName}{expression}";
            }
            else if (LastIsATrigonometricExpression(text, out int trigExpIndex))
            {
                var trigExp = text.Substring(trigExpIndex, text.Length - trigExpIndex);
                if(LastOperandIsNegative(text, out var signIndex))
                {
                    trigExpIndex = signIndex;
                    trigExp = new string(trigExp.Prepend('-').ToArray());
                }
                indexToRemoveFrom = trigExpIndex;
                return $"{FuncName}({trigExp})";
            }
            else if (LastOperandIsNegative(text, out int signIndex))
            {
                var operand = text.Substring(signIndex, text.Length - signIndex);
                indexToRemoveFrom = signIndex;
                return $"{FuncName}({operand})";
            }
            else
            {
                var operand = LastOperand(text);
                indexToRemoveFrom = text.Length - operand.Length;
                return $"{FuncName}({operand})";
            }
        }

        [Version("2.2", Description = "operand also means expression in parenthesis here\n" +
                                        "Preferred to make this method rather than changing NumberBeforeOperator " +
                                        "for the sake of backwards compatibility and prevention of loss of data" +
                                        " while parsing strings to doubles and back to strings")]
        public string LastOperand(string text)
        {
            if(text.Last() == ')' && !LastIsATrigonometricExpression(text, out _))
            {
                int par1_index = FindAMatchingParenthesis(text.Substring(0, text.Length - 1));
                return text.Substring(par1_index, text.Length - par1_index);
            }
            var result = new string("");
            for (var i = 1; !(text.Length < i || IsOperator(text[text.Length - i])); ++i)
            {
                result = result.Insert(0, text[text.Length - i].ToString());
            }

            return result;
        }

        [Version("2.2",Description = "operand also means expression in parenthesis here")]
        public bool LastOperandIsNegative(string text, out int signIndex)
        {
            var operand = LastOperand(text);
            var index = text.Length - operand.Length;
            if(index == 0 || index == text.Length)
            {
                signIndex = text.Length;
                return false;
            }
            else if(index == 1 && text[0] == '-')
            {
                signIndex = index - 1;
                return true;
            }
            if (text[index - 1] == '-' && IsOperator(text[index - 2]))
            {
                signIndex = index - 1;
                return true;
            }
            signIndex = text.Length;
            return false;
        }

        [Version("2.2")]
        public bool LastIsATrigonometricExpression(string text, out int trigExpIndex)
        {
            if (text.Last() == ')' && text.Length >= "sin(n)".Length)
            {
                var index = FindAMatchingParenthesis(text.Substring(0, text.Length - 1));
                if (index < 3)
                {
                    trigExpIndex = text.Length;
                    return false;
                }
                var threeCharsBeforeExpression = text.Substring(index - 3, 3);
                var funcNames = new List<string>()
                {
                    "sin",
                    "cos",
                    "tan",
                    "cot"
                };
                if (funcNames.Any(x => x == threeCharsBeforeExpression))
                {
                    trigExpIndex = index - 3;
                    return true;
                }
            }
            trigExpIndex = text.Length;
            return false;
        }
    }
}
