using DirectPrint;
using DirectPrintFromWebUsingDesktopCli;
using System.Text.RegularExpressions;

namespace OpenCliApplicationFromBrowser
{
    public static class OptionService
    {
        public static void RunOptions(Options options)
        {
            try
            {
                if (options.Open && !string.IsNullOrEmpty(options.Invoice))
                {
                    Console.WriteLine("Fetched Invoice: " + ExtractInvoiceId(options.Invoice));

                    //FETCH DATA FROM LINK
                    //IF DATA FOUND MOVE NEXT

                    string fileFullPath = AssemblyDirectory.GetFilePath();

                    ConvertToPdf.HtmlToPdf();

                    PrintPdf.Print(fileFullPath);
                }
                else if (options.Open)
                {
                    Console.WriteLine("No Invoice found!");
                }
                else
                {
                    Console.WriteLine("No valid commands!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Message: " + e.Message);
            }
        }

        private static string ExtractInvoiceId(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    return "";
                }

                const string pattern = "invoiceid=([A-Za-z0-9-]+)";

                Match match = Regex.Match(url, pattern);

                if (match.Success)
                {
                    return match.Groups[1].Value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong. Message: " + ex.Message);
            }

            return "";
        }
    }
}