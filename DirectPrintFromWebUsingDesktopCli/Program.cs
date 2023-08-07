using CommandLine;
using DirectPrintFromWebUsingDesktopCli;
using OpenCliApplicationFromBrowser;

try
{
    if (!ApiService.GetConfiguration())
    {
        Console.WriteLine("API URL NOT FOUND!");
        return;
    }

    Parser.Default.ParseArguments<Options>(args)
            .WithParsed(OptionService.RunOptions);
}
catch (Exception e)
{
    Console.WriteLine("Something went wrong. Message: " + e.Message);
}