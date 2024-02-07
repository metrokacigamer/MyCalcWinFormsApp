using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Interfaces
{
    public interface INegative
    {
        public void SpecialCheckAndActForNegatives(string expression, ref List<string> operands, ref Dictionary<int, char> operatorsIndexes);
    }
}
