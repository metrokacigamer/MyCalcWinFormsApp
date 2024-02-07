using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp1.Attributes;
using ConsoleApp1.Interfaces;
using ConsoleApp1.Peripherial_Classes;

namespace ConsoleApp1.Functions_Versions
{
    public class Functions : IComputable
    {
        NegativeSignOperations _negatives = new NegativeSignOperations();
        TrigFunctions _trigFuncs = new TrigFunctions();
        Operator _op = new Operator();

        [Version("2.1", Description = "this method essentially gets every Funcs class functionality together," +
            " problem is functionalities' count will increase with the development of the application," +
            " hence the better version of such method will always be needed as long as the app is getting developed," +
            " though I do suspect to have a solution to this problem in my mind right now Im not sure how to execute it" +
            " or whether that solution is of any good," +
            " maybe in the future I will try it if I decide to completely re-design this calculator application", WasUpgradedInVersion = "v2.2")]
        public string ComputeExpressionV2_1(string expression)
        {
            var endIndex = expression.LastIndexOf(')');
            var callGetConditionList = GetConditionList(endIndex, expression);
            var doesntContainParenthesis_2 = callGetConditionList[0];
            var remainingOperandIsExpression = callGetConditionList[1];
            var remainingExpressionIsNegative = callGetConditionList[2];

            if (doesntContainParenthesis_2 || remainingOperandIsExpression || remainingExpressionIsNegative)
            {
                var resultBasedOnCondition = GetResultBasedOnCondition_ComputeExpressionV2_1(doesntContainParenthesis_2, remainingOperandIsExpression, expression);

                return resultBasedOnCondition;
            }
            else
            {
                var result = Compute(expression);

                return result;
            }
        }

        [Version("2.0", Description = "this method should always stay in use and nothing should be altered" +
                                    "(unless there are bugs I didnt account for, but I doubt there are any," +
                                    " code is too simple)",
                                    WasUpgradedInVersion = "v2.1")]
        public string ComputeOperators(string expression)
        {
            bool isEmpty = expression == string.Empty;
            bool containsOperators = !isEmpty && expression.Any(x => Operator.IsOperator(x));

            if (isEmpty || !containsOperators)
            {
                var resultBasedOnCondition = GetResultBasedOnContidition_ComputeOperators(isEmpty, expression);

                return resultBasedOnCondition;
            }
            else
            {
                var result = GetOperationsResult(expression);

                return result;
            }
        }

        private string GetFinalResult(Dictionary<int, char> operatorsIndexes, List<string> operands, string expression)
        {
            _negatives.SpecialCheckAndActForNegatives(expression, ref operands, ref operatorsIndexes);

            var operators = _op.GetOperators(operatorsIndexes);

            var posOfMultAndDiv = _op.GetPosOfMultAndDiv(operators);

            var operatorsAndOperandsAfter = ComputeMultiplicationsAndDivisions(posOfMultAndDiv, operators, operands);
            operatorsAndOperandsAfter = ComputeAdditionsAndSubtractions(operatorsAndOperandsAfter.Item1, operatorsAndOperandsAfter.Item2);

            var result = operatorsAndOperandsAfter.Item2[0];
            return result;
        }

        public string GetOperationsResult(string expression)
        {
            var operands = GenerateOperands(expression);
            var operatorsIndexes = GenerateOperatorsAndIndexesList(expression);

            var result = GetFinalResult(operatorsIndexes, operands, expression);

            return result;
        }

        private string GetResultBasedOnContidition_ComputeOperators(bool isEmpty, string expression)
        {
            if (isEmpty)
            {
                return "0";
            }
            else
            {
                return expression;
            }
        }

        private string GetResultBasedOnCondition_ComputeExpressionV2_1(bool doesntContainParenthesis_2, bool remainingOperandIsExpression, string expression)
        {
            if (doesntContainParenthesis_2)
            {
                return ComputeOperators(expression);
            }
            else if (remainingOperandIsExpression)
            {
                return ComputeExpressionV2_1(expression.Substring(1, expression.Length - 2));
            }
            else
            {
                return "-" + ComputeExpressionV2_1(expression.Substring(2, expression.Length - 3));
            }
        }

        private List<bool> GetConditionList(int endIndex, string expression)
        {
            var conditionList = new List<bool>()
            {
                endIndex == -1
            };

            conditionList.Add(!conditionList[0] && endIndex == expression.Length - 1 && _trigFuncs.FindMatchingParenthesis(expression.Substring(0, expression.Length - 1)) == 0);
            conditionList.Add(!conditionList[0] && !conditionList[1] && endIndex == expression.Length - 1 && _trigFuncs.FindMatchingParenthesis(expression.Substring(0, expression.Length - 1)) == 1);

            return conditionList;
        }

        private List<string> ComputeIndividualExpressions(List<string> expressions)
        {
            var expressionList = new List<string>(expressions);

            for (var i = 0; i < expressionList.Count; ++i)
            {
                if (_trigFuncs.LastIsATrigonometricExpression(expressionList[i], out _))
                {
                    expressionList[i] = ComputeTrigExpressions(expressionList[i]);
                    continue;
                }
                expressionList[i] = ComputeExpressionV2_1(expressionList[i]);
            }

            return expressionList;
        }

        private string GetJoinedExpressionList(List<string> expressionList, List<char> operatorList)
        {
            var joinedExpressionList = new string(string.Empty);
            for (var i = 0; i < expressionList.Count - 1; ++i)
            {
                expressionList[i] += operatorList[i];
                joinedExpressionList += expressionList[i];
            }
            joinedExpressionList += expressionList.Last();

            return joinedExpressionList;
        }

        private string ComputeExpressionsAndGetJoinedExpressionList(string expression)
        {
            var callExpressionOpList = ExpressionOperatorLists(expression);
            var expressionList = callExpressionOpList.Item1;
            var operatorList = callExpressionOpList.Item2;

            expressionList = ComputeIndividualExpressions(expressionList);
            var joinedExpressionList = GetJoinedExpressionList(expressionList, operatorList);

            return joinedExpressionList;
        }

        private string Compute(string expression)
        {
            var joinedExpressionList = ComputeExpressionsAndGetJoinedExpressionList(expression);
            var result = ComputeOperators(joinedExpressionList);

            return result;
        }

        [Version("2.2")]
        public string ComputeTrigExpressions(string str)
        {
            var expressionValue = ComputeExpressionV2_1(str.Substring(3));

            if (str.Substring(0, 3) == _trigFuncs._sin)
            {
                return Math.Sin(double.Parse(expressionValue)).ToString();
            }
            else if (str.Substring(0, 3) == _trigFuncs._cos)
            {
                return Math.Cos(double.Parse(expressionValue)).ToString();
            }
            else if (str.Substring(0, 3) == _trigFuncs._tan)
            {
                return Math.Tan(double.Parse(expressionValue)).ToString();
            }
            else
            {
                return (1 / Math.Tan(double.Parse(expressionValue))).ToString();
            }
        }

        [Version("2.1")]
        public (List<char>, List<string>) ComputeAdditionsAndSubtractions(List<char> operators, List<string> operands)
        {
            for (int op = 0; op < operators.Count; ++op)
            {
                var result = string.Empty;
                var firstOperandIndex = op;
                var secondOperandIndex = op + 1;

                if (operators[op] == _op._plus)
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
            return (operators, operands);
        }

        [Version("2.1", Description = "this method should be called before the very last operation on expression" +
                                     "(which is AdditionsAndSubtractions() )" +
                                     " these methods in theory should also always stay in use")]
        public (List<char>, List<string>) ComputeMultiplicationsAndDivisions(List<int> posOfMultAndDiv, List<char> operators, List<string> operands)
        {
            foreach (var op in posOfMultAndDiv)
            {
                var result = string.Empty;
                var firstOperandIndex = op;
                var secondOperandIndex = op + 1;

                if (operators[op] == _op._asterisk)
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
            return (operators, operands);
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
        public List<string> GenerateOperands(string str)
        {
            var separatorChars = new char[]
            {
                _op._plus,
                _op._minus,
                _op._asterisk,
                _op._backslash
            };
            var operands = str.Split(separatorChars)
                                .ToList();
            operands.RemoveAll(x => x == string.Empty);
            return operands;
        }

        [Version("2.1", Description = "method added in v2.1 to improve code readability")]
        public Dictionary<int, char> GenerateOperatorsAndIndexesList(string str)
        {
            var dictionary1 = str.Select((character, index) => new { character, index })
                                .Where(x => Operator.IsOperator(x.character))
                                .ToDictionary(x => x.index, x => x.character);
            return dictionary1;
        }

        private List<string> GetOperandsUnfixed(string expression)
        {
            var result = new List<string>();

            var endIndex = 0;
            var startIndex = 0;

            while (!expression.Equals(string.Empty))
            {
                endIndex = expression.LastIndexOf(_trigFuncs._parenthesis_2);
                startIndex = _trigFuncs.FindMatchingParenthesis(expression.Substring(0, Math.Abs(endIndex)));

                var stringAfterLastParenthesis_2 = expression.Substring(endIndex + 1);
                var containsStringAfterLastParenthesis_2 = stringAfterLastParenthesis_2 != string.Empty;
                var temporary = GetResultBasedOnCondition_For_GetOperandsUnfixed(containsStringAfterLastParenthesis_2, expression, result, startIndex, endIndex);

                result = temporary.Item1;
                expression = temporary.Item2;
            }

            result.Reverse();

            return result;
        }

        private (List<string>, string) GetResultBasedOnCondition_For_GetOperandsUnfixed(bool containsStringAfterLastParenthesis_2, string expression, List<string> result, int startIndex, int endIndex)
        {
            if (containsStringAfterLastParenthesis_2)
            {
                var stringAfterParenthesis_2 = expression.Substring(endIndex + 1);
                var stringUntilParenthesis_2 = expression.Substring(0, endIndex + 1);

                result.Add(stringAfterParenthesis_2);
                expression = stringUntilParenthesis_2;
            }
            else
            {
                var lastOperandIsATrigExpression = _trigFuncs.LastIsATrigonometricExpression(expression, out var trigExpIndex);
                startIndex = lastOperandIsATrigExpression ? trigExpIndex : startIndex;
                var lastExpression = expression.Substring(startIndex, endIndex + 1 - startIndex);
                var expressionRemainder = expression.Substring(0, startIndex);

                result.Add(lastExpression);
                expression = expressionRemainder;
            }

            return (result, expression);
        }

        private List<string> RemoveIfEmptyAtIndex(List<string> result, int index)
        {
            if (result[index] == string.Empty)
            {
                result.Remove(string.Empty);
            }
            return result;
        }

        private (List<string>, List<char>) GetAlteredContentBasedOnConditions((List<string>, List<char>) expressionsAndOperators, int index, bool firstIsOperator, bool lastAndPreLastAreOperators, bool lastIsOperatorPreLastIsNot)
        {
            var expressions = expressionsAndOperators.Item1;
            var operators = expressionsAndOperators.Item2;
            if (firstIsOperator)
            {
                var operatorAtFirst = expressions[index].First();
                operators.Add(operatorAtFirst);

                expressions[index] = expressions[index].Remove(0, 1);
            }
            else if (lastAndPreLastAreOperators)
            {
                var operatorBeforeLast = expressions[index][expressions[index].Length - 2];
                operators.Add(operatorBeforeLast);

                var minusPrepended = $"{_op._minus}{_trigFuncs._parenthesis_1}";
                var nextExpressionLastIndex = expressions[index + 1].Length - 1 + 2;//plus two for extra minusPrepended string
                var currentExpressionPreLastIndex = expressions[index].Length - 2;

                expressions[index + 1] = expressions[index + 1].Insert(0, minusPrepended);
                expressions[index + 1] = expressions[index + 1].Insert(nextExpressionLastIndex, _trigFuncs._parenthesis_2.ToString());
                expressions[index] = expressions[index].Remove(currentExpressionPreLastIndex, 2);
            }
            else if (lastIsOperatorPreLastIsNot)
            {
                var operatorAtLast = expressions[index].Last();
                operators.Add(operatorAtLast);

                expressions[index] = expressions[index].Remove(expressions[index].Length - 1, 1);
            }

            expressions = RemoveIfEmptyAtIndex(expressions, index);


            return (expressions, operators);
        }

        private (List<string>, List<char>) GetOperandsAndOperatorsFixed(List<string> expressions)
        {
            var operators = new List<char>();
            var pairOfExpressionsAndOperators = (expressions, operators);
            bool skipNext = false;
            for (var i = 0; i < expressions.Count; ++i)
            {
                if (skipNext)
                {
                    skipNext = false;

                    continue;
                }

                pairOfExpressionsAndOperators = FixExpressionsAndOperands(pairOfExpressionsAndOperators, i, out skipNext);
            }

            return pairOfExpressionsAndOperators;
        }

        private (List<string> expressions, List<char> operators) FixExpressionsAndOperands((List<string> expressions, List<char> operators) pairOfExpressionsAndOperators, int index, out bool skipNext)
        {
            pairOfExpressionsAndOperators = ChangeIfFirstIsOperator(pairOfExpressionsAndOperators, index);

            var lastIsOperator = Operator.IsOperator(pairOfExpressionsAndOperators.expressions[index].Last());
            var lastAndPreLastAreOperators = lastIsOperator && Operator.IsOperator(pairOfExpressionsAndOperators.expressions[index][pairOfExpressionsAndOperators.expressions[index].Length - 2]);

            skipNext = lastAndPreLastAreOperators ? true : false;

            pairOfExpressionsAndOperators = ChangeIfLastIsOperator(pairOfExpressionsAndOperators, index);

            return pairOfExpressionsAndOperators;
        }

        private (List<string> expressions, List<char>) ChangeIfFirstIsOperator((List<string> expressions, List<char>) pairOfExpressionsAndOperators, int i)//, bool firstIsOperator)
        {
            var firstIsOperator = Operator.IsOperator(pairOfExpressionsAndOperators.expressions[i].First());

            if (firstIsOperator)
            {
                pairOfExpressionsAndOperators = GetAlteredContentBasedOnConditions(pairOfExpressionsAndOperators, i, firstIsOperator, false, false);
            }

            return pairOfExpressionsAndOperators;
        }

        private (List<string> expressions, List<char>) ChangeIfLastIsOperator((List<string> expressions, List<char>) pairOfExpressionsAndOperators, int i)//, bool lastAndPreLastAreOperators, bool lastIsOperatorPreLastIsNot)
        {
            var lastAndPreLastAreOperators = Operator.IsOperator(pairOfExpressionsAndOperators.expressions[i].Last())
                                                                    && Operator.IsOperator(pairOfExpressionsAndOperators.expressions[i][pairOfExpressionsAndOperators.expressions[i].Length - 2]);
            var lastIsOperatorPreLastIsNot = !lastAndPreLastAreOperators && Operator.IsOperator(pairOfExpressionsAndOperators.expressions[i].Last());

            if (lastAndPreLastAreOperators)
            {
                pairOfExpressionsAndOperators = GetAlteredContentBasedOnConditions(pairOfExpressionsAndOperators, i, false, lastAndPreLastAreOperators, lastIsOperatorPreLastIsNot);
            }
            else if (lastIsOperatorPreLastIsNot)
            {
                pairOfExpressionsAndOperators = GetAlteredContentBasedOnConditions(pairOfExpressionsAndOperators, i, false, lastAndPreLastAreOperators, lastIsOperatorPreLastIsNot);
            }

            return pairOfExpressionsAndOperators;
        }

        [Version("2.1", Description = "this method essentially describes the computation process of the expression " +
                                    "in given application, which is " +
                                    " first are calculated expressions in parenthesis, once only the numbers and operators are" +
                                    " left the expression is passed to ComputeOperators() method. Though better version" +
                                    " of ComputeExpressionV2_1() is needed in v2.2+ to support built in expressions" +
                                    " such as trigonometric, this specific method should always stay in use")]
        public (List<string>, List<char>) ExpressionOperatorLists(string expression)
        {
            var dummy_expression = new string(expression);
            var expressions = GetOperandsUnfixed(dummy_expression);

            var temp = GetOperandsAndOperatorsFixed(expressions);
            expressions = temp.Item1;
            var ops = temp.Item2;


            return (expressions, ops);
        }
    }
}