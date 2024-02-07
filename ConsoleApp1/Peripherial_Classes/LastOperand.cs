using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Attributes;

namespace ConsoleApp1.Peripherial_Classes
{
    public class LastOperand : Parenthesis
    {
        public bool LastOperandContainsDecimal(string expr)
        {
            if (!expr.Contains('.'))
            {
                return false;
            }
            var anonVar = expr.Select((x, i) => new { Character = x, Index = i })
                            .LastOrDefault(x => Operator.IsOperator(x.Character));
            var index = anonVar == default ? 0 : anonVar.Index;
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

        [Version("2.2", Description = "implementation now much better compared to its previous", WasUpgradedInVersion = "v2.3")]
        public string GetLastOperand(string text)
        {
            if (text.Last() == ')')
            {
                int par1_index = FindMatchingParenthesis(text.Substring(0, text.Length - 1));
                var checkForDefault = text.Substring(0, par1_index + 1)
                                            .Select((x, i) => new { character = x, index = i })
                                            .LastOrDefault(x => Operator.IsOperator(x.character));
                var index = 0;
                if (checkForDefault != default)
                {
                    index = checkForDefault.index + 1;
                }

                return text.Substring(index, text.Length - index);
            }
            else
            {
                var checkForDefault = text.Select((x, i) => new { character = x, index = i })
                                            .LastOrDefault(x => Operator.IsOperator(x.character));
                var index = 0;
                if (checkForDefault != default)
                {
                    index = checkForDefault.index + 1;
                }

                return text.Substring(index, text.Length - index);
            }
        }
    }
}
