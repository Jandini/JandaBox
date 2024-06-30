using System.Reflection;

namespace ConsoleBox
{
    internal class About
    {
        public string ApplicationName { get; set; }
        public string InformationalVersion { get; set; }
        public override string ToString() => ApplicationName + ' ' + InformationalVersion;

        public static About Program = new()
        {
            ApplicationName = "ConsoleBox",
            InformationalVersion = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                .InformationalVersion
        };
    }
}
