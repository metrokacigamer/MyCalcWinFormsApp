using System;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var methodInfo = typeof(Funcs).GetMethod("ComputeOperators");

            var attribute = Attribute.GetCustomAttribute(methodInfo, typeof(VersionAttribute));
            if (attribute != null)
            {
                var version = ((VersionAttribute)attribute).Version;
                var description = ((VersionAttribute)attribute).Description;
                Console.WriteLine($"Version: {version}, Description: {description}{Environment.NewLine}Is Upgraded from previous versions: {((VersionAttribute)attribute).IsUpgradedFromPrevVersions}");
            }
        }
    }
}
