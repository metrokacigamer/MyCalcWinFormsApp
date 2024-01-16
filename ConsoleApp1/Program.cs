using System;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var string1 = "848.87841548949421";
            var index1 = string1.Select((x, i) => new { Character = x, Index = i })
                                .LastOrDefault(x => x.Character == '.')
                                .Index;
            var string2 = "9987.234";
            var index2 = string2.Select((x, i) => new { Character = x, Index = i })
                    .LastOrDefault(x => x.Character == '.')
                    .Index;
            Console.WriteLine(string1.Substring(0, index1 + 9));
            Console.WriteLine(string2.Substring(0, index2 + 9));
        }
    }
}
