using CommandLine;
using DirectPrint;
using OpenCliApplicationFromBrowser;

Console.WriteLine("Starting the application.");

try
{
    Parser.Default.ParseArguments<Options>(args)
            .WithParsed(OptionService.RunOptions);
}
catch (Exception e)
{
    Console.WriteLine("Something went wrong. Message: " + e.Message);
}

Console.ReadLine();