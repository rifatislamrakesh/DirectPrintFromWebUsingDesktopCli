using Spire.Pdf;

namespace DirectPrint
{
    public static class PrintPdf
    {
        public static bool Print(string pdfFilePath)
        {
            try
            {
                PdfDocument doc = new();
                doc.LoadFromFile(pdfFilePath);
                doc.Print();

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