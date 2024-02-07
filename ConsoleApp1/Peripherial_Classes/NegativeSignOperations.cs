using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Attributes;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Peripherial_Classes
{
    public class NegativeSignOperations : LastOperand, INegative
    {
        [Version("2.1", Description = "It is probably better to have this method used" +
                            " on the expression before finally passing it to the ComputeOperators() method")]
        public void SpecialCheckAndActForNegatives(string expression, ref List<string> operands, ref Dictionary<int, char> operatorsIndexes)
        {
            if (operatorsIndexes.Count >= operands.Count)
            {
                var temp = ActIfFirstOperatorHasKeyZero(operatorsIndexes, operands);
                operatorsIndexes = temp.Item1;
                operands = temp.Item2;

                temp = ActIfAnyTwoOperatorsAreInSequence(expression, operatorsIndexes, operands);
                operatorsIndexes = temp.Item1;
                operands = temp.Item2;
            }
        }

        (Dictionary<int, char>, List<string>) ActIfFirstOperatorHasKeyZero(Dictionary<int, char> operatorsIndexes, List<string> operands)
        {
            if (operatorsIndexes.Keys.ToList()[0] == 0)
            {
                operands[0] = PrependMinusToOperand(operands[0]);
                operatorsIndexes.Remove(operatorsIndexes.Keys.ToList()[0]);
            }
            return (operatorsIndexes, operands);
        }

        string PrependMinusToOperand(string operand)
        {
            return new string(operand.Prepend('-')
                                .ToArray());
        }

        Dictionary<int, char> GetDictionaryWithRemovedValueAtIndex(int index, Dictionary<int, char> operatorsIndexes)
        {
            var keyOfValue = operatorsIndexes.Keys.ToList()[index];
            operatorsIndexes.Remove(keyOfValue);

            return operatorsIndexes;
        }

        (Dictionary<int, char>, List<string>) ActIfAnyTwoOperatorsAreInSequence(string initialExpr, Dictionary<int, char> operatorsIndexes, List<string> operands)
        {
            for (var count = 0; count < operatorsIndexes.Count;)
            {
                var TwoOperatorsAreInSequence = Operator.IsOperator(initialExpr[operatorsIndexes.Keys.ToList()[count] - 1]);
                if (TwoOperatorsAreInSequence)
                {
                    operands[count] = PrependMinusToOperand(operands[count]);
                    operatorsIndexes = GetDictionaryWithRemovedValueAtIndex(count, operatorsIndexes);
                }
                else
                {
                    ++count;
                }
            }

            return (operatorsIndexes, operands);
        }

        [Version("2.2", Description = "operand can also mean expression in parenthesis here")]
        public bool LastOperandIsNegative(string text, out int signIndex)
        {
            var operand = GetLastOperand(text);
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
            if (text[index - 1] == '-' && Operator.IsOperator(text[index - 2]))
            {
                signIndex = index - 1;

                return true;
            }
            signIndex = text.Length;

            return false;
        }
    }
}
