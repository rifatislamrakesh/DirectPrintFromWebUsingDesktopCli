using CommandLine;
using OpenCliApplicationFromBrowser;

try
{
    Parser.Default.ParseArguments<Options>(args)
            .WithParsed(OptionService.RunOptions);
}
catch (Exception e)
{
    Console.WriteLine("Something went wrong. Message: " + e.Message);
}

//Console.ReadLine();