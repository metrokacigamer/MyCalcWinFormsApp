using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class VersionAttribute : Attribute
    {
        public string WasAddedInVersion { get; } = string.Empty;
        public string WasUpgradedInVersion { get; set; }
        public string Description { get; init; } = "Attribute contains no description";
        public bool IsUpgradedFromPrevVersions { get; init; } = false;
        public bool IsInUse { get; set; } = true;

        public VersionAttribute(string version)
        {
            WasAddedInVersion = $"v{version}";
            WasUpgradedInVersion = WasAddedInVersion;
        }
    }
}
