using System;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var var1 = double.Parse("231999.248");
            var str = "123456";
            str = new string(str.Prepend('-').ToArray());
            Console.WriteLine(str);

        }
    }
}
