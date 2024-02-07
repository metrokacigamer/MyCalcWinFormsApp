using ConsoleApp1.Attributes;
using ConsoleApp1.Interfaces;
using ConsoleApp1.Peripherial_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Expression_Formatting
{
    public class ExpressionFormatting: NegativeSignOperations
    {
        public string GetFormattedDecimalString(string text)
        {
            if (LastOperandContainsDecimal(text))
            {
                return string.Empty;
            }
            else if (text == string.Empty || Operator.IsOperator(text.Last()))
            {
                return "0.";
            }
            else
            {
                return ".";
            }
        }

        [Version("2.2", Description = "added in v2.2")]
        public string GetFormattedEndingToTheInput(string text)
        {
            if (text.EndsWith('/') || text.EndsWith('*'))
            {
                return "1";
            }
            else if (text.EndsWith('.') || text == string.Empty || Operator.IsOperator(text.Last()))
            {
                return "0";
            }
            return string.Empty;
        }

        [Version("2.1", Description = "given method essentially returns value in different format" +
                            " depending on its value, whether its too \"short\" or too \"long\"")]
        public string GetFormattedOperand(string res)
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

            if (index == default && res.Length > maxIndex || index > maxIndex)
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
    }
}
