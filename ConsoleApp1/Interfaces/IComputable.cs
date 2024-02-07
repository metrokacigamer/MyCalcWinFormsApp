using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Interfaces
{
    public interface IComputable
    {
        public string Subtraction(string v1, string v2);

        public string Addition(string v1, string v2);

        public string Division(string v1, string v2);

        public string Multiplication(string v1, string v2);

        public (List<char>, List<string>) ComputeMultiplicationsAndDivisions(List<int> posOfMultAndDiv, List<char> operators, List<string> operands);

        public (List<char>, List<string>) ComputeAdditionsAndSubtractions(List<char> operators, List<string> operands);
        public string ComputeOperators(string expression);


    }
}
