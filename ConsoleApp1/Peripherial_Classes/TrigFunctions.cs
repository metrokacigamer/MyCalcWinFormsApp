using ConsoleApp1.Attributes;

namespace ConsoleApp1.Peripherial_Classes
{
    public class TrigFunctions : NegativeSignOperations
    {
        public string _sin = "sin";
        public string _cos = "cos";
        public string _tan = "tan";
        public string _cot = "cot";

        [Version("2.2")]
        public string GetFormattedTrigFunc(string text, string FuncName, out int indexToRemoveFrom)
        {
            if (text == string.Empty || text.Last() == '(')// "+|-123",
            {
                indexToRemoveFrom = text.Length;
                return $"{FuncName}(0)";
            }
            else if (Operator.IsOperator(text.Last()))
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
                var operand = GetLastOperand(text);
                indexToRemoveFrom = text.Length - operand.Length;
                return $"{FuncName}({operand})";
            }
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
    }
}
