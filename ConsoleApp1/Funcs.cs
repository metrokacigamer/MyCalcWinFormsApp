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
        [Version("2.1", Description = "this method essentially gets every Funcs class functionality together," +
            " problem is functionalities' count will increase with the development of the application," +
            " hence the better version of such method will always be needed as long as the app is getting developed," +
            " though I do suspect to have a solution to this problem in my mind right now Im not sure how to execute it" +
            " or whether that solution is of any good," +
            " maybe in the future I will try it if I decide to completely re-design this calculator application")]
        public string ComputeExpressionV2_1(string str)
        {
            var endIndex = str.LastIndexOf(')');
            if (endIndex == -1)
            {
                return ComputeOperators(str);
            }
            else if ((endIndex == str.Length - 1) && (FindMatchingParenthesis(str.Substring(0, str.Length - 1)) == 0))
            {
                return ComputeExpressionV2_1(str.Substring(1, str.Length - 2));
            }
            else if ((endIndex == str.Length - 1) && (FindMatchingParenthesis(str.Substring(0, str.Length - 1)) == 1))
            {
                return "-" + ComputeExpressionV2_1(str.Substring(2, str.Length - 3));
            }

            var callExpressionOpList = ExpressionOpList(str);
            var expressionList = callExpressionOpList.Item1;
            var operatorList = callExpressionOpList.Item2;

            for (var i = 0; i < expressionList.Count; ++i)
            {
                if (LastIsATrigonometricExpression(expressionList[i], out _))
                {
                    expressionList[i] = ComputeTrigExpressions(expressionList[i]);
                    continue;
                }
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

        [Version("2.0", Description = "this method should always stay in use and nothing should be altered" +
                                    "(unless there are bugs I didnt account for, but I doubt there are any," +
                                    " code is too simple)",
                                    WasUpgradedInVersion = "v2.1")]
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

        public string ComputeTrigExpressions(string str)
        {
            var expressionValue = ComputeExpressionV2_1(str.Substring(3));
            if (str.Substring(0, 3) == "sin")
            {
                return Math.Sin(double.Parse(expressionValue)).ToString();
            }
            else if (str.Substring(0, 3) == "cos")
            {
                return Math.Cos(double.Parse(expressionValue)).ToString();
            }
            else if (str.Substring(0, 3) == "tan")
            {
                return Math.Tan(double.Parse(expressionValue)).ToString();
            }
            else
            {
                return (1 / Math.Tan(double.Parse(expressionValue))).ToString();
            }
        }
        [Version("2.1")]
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

        [Version("2.1", Description = "this method should be called before the very last operation on expression" +
                                     "(which is AdditionsAndSubtractions() )" +
                                     " these methods in theory should also always stay in use")]
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

        [Version("2.1")]
        public string Subtraction(string v1, string v2)
        {
            var result = (double.Parse(v1) - double.Parse(v2)).ToString();
            return result;
        }

        [Version("2.1")]
        public string Addition(string v1, string v2)
        {
            var result = (double.Parse(v1) + double.Parse(v2)).ToString();
            return result;
        }

        [Version("2.1")]
        public string Division(string v1, string v2)
        {
            var result = (double.Parse(v1) / double.Parse(v2)).ToString();
            return result;
        }

        [Version("2.1", Description = "these methods for simple arithmetics were added in v2.1," +
                                " the intention is to stop parsing strings into doubles for computation" +
                                " sometime into the future," +
                                " Maybe I will write a code that does simple math operations" +
                                " on strings without casting them into doubles to increase accuracy," +
                                " but that is too much of a headache for the time being")]
        public string Multiplication(string v1, string v2)
        {
            var result = (double.Parse(v1) * double.Parse(v2)).ToString();
            return result;
        }

        [Version("2.1", Description = "method added in v2.1 to improve code readability")]
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

        [Version("2.1", Description = "method added in v2.1 to improve code readability")]
        private List<(char character, int index)> GenerateOperatorsAndIndexesList(string str)
        {
            var result = str.Select((character, index) => (character, index))
                            .Where(x => IsOperator(x.character))
                            .ToList();
            return result;
        }

        [Version("2.1", Description = "It is probably better to have this method used" +
                                    " on the expression before finally passing it to the ComputeOperators() method")]
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

        [Version("2.1", Description = "this method essentially describes the computation process of the expression " +
                                    "in given application, which is " +
                                    " first are calculated expressions in parenthesis, once only the numbers and operators are" +
                                    " left the expression is passed to ComputeOperators() method. Though better version" +
                                    " of ComputeExpressionV2_1() is needed in v2.2+ to support built in expressions" +
                                    " such as trigonometric, this specific method should always stay in use")]
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
                startIndex = FindMatchingParenthesis(tempStr.Substring(0, Math.Abs(endIndex)));
                if (tempStr.Substring(endIndex + 1) != string.Empty)
                {
                    result.Add(tempStr.Substring(endIndex + 1));
                    tempStr = tempStr.Substring(0, endIndex + 1);
                }
                else
                {
                    if (LastIsATrigonometricExpression(tempStr, out var trigExpIndex))
                    {
                        startIndex = trigExpIndex;
                    }
                    result.Add(tempStr.Substring(startIndex, endIndex + 1 - startIndex));
                    tempStr = tempStr.Substring(0, startIndex);
                }
            }
            result.Reverse();
            bool skipNext = false;
            for (var i = 0; i < result.Count; ++i)
            {
                if (skipNext)
                {
                    continue;
                }

                if (IsOperator(result[i].First()))
                {
                    ops.Add(result[i].First());
                    result[i] = result[i].Remove(0, 1);
                    if (result[i] == string.Empty)
                    {
                        result.Remove("");
                    }
                    skipNext = false;
                }

                if (IsOperator(result[i].Last()) && IsOperator(result[i][result[i].Length - 2]))
                {
                    ops.Add(result[i][result[i].Length - 2]);
                    result[i + 1] = result[i + 1].Insert(0, "-(");
                    result[i + 1] = result[i + 1].Insert(result[i + 1].Length - 1, ")");
                    result[i] = result[i].Remove(result[i].Length - 2, 2);
                    if (result[i] == string.Empty)
                    {
                        result.Remove("");
                    }
                    skipNext = true;
                }
                else if (IsOperator(result[i].Last()))
                {
                    ops.Add(result[i].Last());
                    result[i] = result[i].Remove(result[i].Length - 1, 1);
                    if (result[i] == string.Empty)
                    {
                        result.Remove("");
                    }
                    skipNext = false;
                }
            }
            return (result, ops);
        }

        [Version("2.1", Description = "given method essentially returns value in different format" +
                                    " depending on its value, whether its too \"short\" or too \"long\"")]
        public string StringToReturn(string res)
        {
            var numbersAfterDecimal = 11;
            var index = 0;
            const int maxIndex = 15;
            if (LastOperandContainsDecimal(res))
            {
                index = res.Select((x, i) => new { Character = x, Index = i })
                                    .LastOrDefault(x => x.Character == '.')
                                    .Index;
            }

            if ((index == default && res.Length > maxIndex) || index > maxIndex)
            {
                return $"{res[0]}.{res[1]}{res[2]}{res[3]}e+{index}";
            }
            else if (res.Length - index > numbersAfterDecimal)
            {
                return res.Substring(0, index + 1 + numbersAfterDecimal);
            }
            else
            {
                return res;
            }
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

        [Version("1.0", Description = "this is an initial method for computing the expression passed" +
                                        " by buttonEquals eventhandler," +
                                        " due to its poor design and performance its been left behind", IsInUse = false)]
        public string ComputeExpression(string str)
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

        [Version("1.0", IsInUse = false)]
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

        [Version("2.1")]
        public string DecideHowToAddParenthesis_2(string text)
        {
            if (HasMatchingParenthesis_1(text, out _) || !IsOperator(text.Last()))
            {
                return ")";
            }
            else
            {
                return string.Empty;
            }
        }

        [Version("2.1")]
        public bool HasMatchingParenthesis_1(string text, out int index)
        {
            index = default;
            if (text.Count(x => x == '(') > text.Count(x => x == ')'))
            {
                index = FindMatchingParenthesis(text);
                return true;
            }
            else
            {
                return false;
            }
        }

        [Version("2.1")]
        public int FindMatchingParenthesis(string text)
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
                var index = FindMatchingParenthesis(text.Substring(0, text.Length - 1));
                var expression = text.Substring(index, text.Length - index);
                if (LastOperandIsNegative(text, out var signIndex))
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
                if (LastOperandIsNegative(text, out var signIndex))
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

        [Version("2.2", Description = "operand can also mean expression in parenthesis here\n" +
                                        "Preferred to create this new method rather than changing NumberBeforeOperator " +
                                        "for the sake of backwards compatibility and prevention of loss of data" +
                                        " while parsing strings to doubles and back to strings")]
        public string LastOperand(string text)
        {
            if (text.Last() == ')' && !LastIsATrigonometricExpression(text, out _))
            {
                int par1_index = FindMatchingParenthesis(text.Substring(0, text.Length - 1));
                return text.Substring(par1_index, text.Length - par1_index);
            }
            var result = new string("");
            for (var i = 1; !(text.Length < i || IsOperator(text[text.Length - i])); ++i)
            {
                result = result.Insert(0, text[text.Length - i].ToString());
            }

            return result;
        }

        [Version("2.2", Description = "operand can also mean expression in parenthesis here")]
        public bool LastOperandIsNegative(string text, out int signIndex)
        {
            var operand = LastOperand(text);
            var index = text.Length - operand.Length;
            if (index == 0 || index == text.Length)
            {
                signIndex = text.Length;
                return false;
            }
            else if (index == 1 && text[0] == '-')
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
                var index = FindMatchingParenthesis(text.Substring(0, text.Length - 1));
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

        [Version("2.2", Description = "added in v2.2")]
        public string ProperFinishToTheInput(string text)
        {
            if (text.EndsWith('/') || text.EndsWith('*'))
            {
                return "1";
            }
            else if (text.EndsWith('.') || text == string.Empty || IsOperator(text.Last()))
            {
                return "0";
            }
            return string.Empty;
        }

        [Version("2.2", Description = "used for checking the expression before passing it to the expression computing method")]
        public bool HasUnmatchedParenthesis(string text)
        {
            if (text.Count(x => x == '(') > text.Count(x => x == ')'))
            {
                return true;
            }
            return false;
        }
    }
}
