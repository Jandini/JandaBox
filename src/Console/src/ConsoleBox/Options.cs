using CommandLine;

class Options
{
    [Verb("run", isDefault: true, HelpText = "Run program.")]
    internal class Run
    {
#if (settings)
        [Option('p', "path", HelpText = "Directory path.", Required = true)]
        public string Path { get; set; }
#endif
    }
}
