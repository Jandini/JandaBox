using CommandLine;

internal sealed class Options
{
    [Verb("run", isDefault: true, HelpText = "Run program.")]
    internal class Run
    {
#if (options)
        [Option('p', "path", HelpText = "Directory path.", Required = true)]
        public string Path { get; set; }
#endif
    }
}
