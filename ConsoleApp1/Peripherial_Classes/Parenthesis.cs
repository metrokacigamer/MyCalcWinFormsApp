using ConsoleApp1.Attributes;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Peripherial_Classes
{
    public class Parenthesis : IAppendableParenthesis
    {
        public char _parenthesis_1 = '(';
        public char _parenthesis_2 = ')';

        [Version("2.1", Description = "below methods are used only in Form1.cs")]
        public string GetFormattedParenthesis_1(string text)
        {
            if (text == string.Empty || Operator.IsOperator(text.Last()) || text.Last() == '(')
            {
                return "(";
            }
            else
            {
                return "*(";
            }
        }

        [Version("2.1")]
        public string GetFormattedParenthesis_2(string text)
        {
            if (HasMatchingParenthesis_1(text, out _) || !Operator.IsOperator(text.Last()))
            {
                return ")";
            }
            else
            {
                return string.Empty;
            }
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
    }
}
