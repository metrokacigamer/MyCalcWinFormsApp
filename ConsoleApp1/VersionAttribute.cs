using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [AttributeUsage(AttributeTargets.Method)]
    public class VersionAttribute: Attribute
    {
        public string Version { get; } = string.Empty;
        public string Description { get; init; } = "Attribute contains no description";

        public bool IsUpgradedFromPrevVersions { get; init; } = false;
        p
        public bool IsInUse { get; set; } = true;
        public VersionAttribute(string version)
        {
            Version = $"v{version}";
        }
    }
}
