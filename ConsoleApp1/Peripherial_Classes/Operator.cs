using ConsoleApp1.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Peripherial_Classes
{
    public class Operator
    {
        public char _plus = '+';
        public char _minus = '-';
        public char _asterisk = '*';
        public char _backslash = '/';


        [Version("2.3")]
        public List<char> GetOperators(Dictionary<int, char> operatorsIndexes)
        {
            var operators = operatorsIndexes.Select(x => x.Value)
                                .ToList();

            return operators;
        }

        [Version("2.3")]
        public List<int> GetPosOfMultAndDiv(List<char> operators)
        {
            var posOfMultAndDiv = operators.Select((x, i) => new { Character = x, Index = i })
                                .Where(x => x.Character == '*' || x.Character == '/')
                                .Select(x => x.Index)
                                .ToList();

            return posOfMultAndDiv;
        }

        public static bool IsOperator(char el)
        {
            return el == '+' || el == '-' || el == '*' || el == '/';
        }
    }
}
