using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str = "dasda";
            var abb = str.Where(x => true);
            var str2 = abb.ToArray();
            Console.WriteLine(new string(str2.Where(x => true).ToArray()));
        }
    }
}
