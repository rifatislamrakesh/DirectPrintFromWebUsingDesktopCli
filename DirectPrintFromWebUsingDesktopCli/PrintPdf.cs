using Spire.Pdf;

namespace DirectPrint
{
    public static class PrintPdf
    {
        public static bool Print(string pdfFilePath)
        {
            try
            {
                Console.WriteLine("Printing...");

                PdfDocument doc = new();
                doc.LoadFromFile(pdfFilePath);
                doc.Print();

                Console.WriteLine("Print Successful.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}