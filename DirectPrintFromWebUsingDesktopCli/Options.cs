using CommandLine;

namespace OpenCliApplicationFromBrowser
{
    public class Options
    {
        [Option("open", Required = false, HelpText = "Open the application.")]
        public bool Open { get; set; }

        [Option("invoice", Required = false, HelpText = "Invoice number.")]
        public string Invoice { get; set; } = string.Empty;
    }
}