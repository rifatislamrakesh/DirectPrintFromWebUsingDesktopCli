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
                    string invoiceId = ExtractInvoiceId(options.Invoice);

                    if (string.IsNullOrEmpty(invoiceId))
                    {
                        Console.WriteLine("No Invoice ID found!");
                        return;
                    }

                    ReceiptModel data = ApiService.GetData(invoiceId).GetAwaiter().GetResult();
                    if (data is null)
                    {
                        Console.WriteLine("No data received!");
                        return;
                    }

                    /* Uncomment if needed to save the file
                    string fileFullPath = AssemblyDirectory.GetFilePath();
                    if (string.IsNullOrEmpty(fileFullPath))
                    {
                        Console.WriteLine("Path not found/error");
                        return;
                    }

                    ConvertToPdf.SavePdf(data, fileFullPath);
                    PrintPdf.Print(fileFullPath);
                    */

                    ConvertToPdf.ConvertAndPrint(data);
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