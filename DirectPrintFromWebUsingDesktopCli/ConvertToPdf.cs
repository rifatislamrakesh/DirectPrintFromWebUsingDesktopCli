using DirectPrintFromWebUsingDesktopCli;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.HtmlConverter;
using Spire.Pdf.HtmlConverter.Qt;
using System.Drawing;
using System.Text;

namespace DirectPrint
{
    public static class ConvertToPdf
    {
        public static bool HtmlToPdf()
        {
            try
            {
                DirectDraw();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong when printing. Message:" + e.Message);
                return false;
            }
        }

        private static void DirectDraw()
        {
            // Create an instance of your Invoice model with sample data
            var invoice = new Invoice
            {
                InvoiceNumber = "INV-12345",
                CustomerName = "John Doe",
                InvoiceDate = DateTime.Now,
                InvoiceItems = new List<InvoiceItem>
                {
                    new InvoiceItem { ItemName = "Item 1", Price = 10.0m, Quantity = 2 },
                    new InvoiceItem { ItemName = "Item 2", Price = 15.0m, Quantity = 1 },
                    new InvoiceItem { ItemName = "Item 3", Price = 5.0m, Quantity = 3 },
                }
            };

            // Create a new PDF document
            PdfDocument pdf = new();

            // Add a new page to the document
            PdfPageBase page = pdf.Pages.Add();

            // Create a font for the text
            PdfFont font = new(PdfFontFamily.Helvetica, 25f);

            // Create a brush to define the text color
            PdfSolidBrush brush = new(Color.Black);

            // Define the initial position for the content
            float x = 0f;
            float y = 0f;

            // Draw invoice details
            page.Canvas.DrawString($"Invoice Number: {invoice.InvoiceNumber}", font, brush, x, y);
            y += 20;
            page.Canvas.DrawString($"Customer Name: {invoice.CustomerName}", font, brush, x, y);
            y += 20;
            page.Canvas.DrawString($"Invoice Date: {invoice.InvoiceDate:yyyy-MM-dd}", font, brush, x, y);
            y += 40;

            // Draw invoice items table headers
            page.Canvas.DrawString("Item Name", font, brush, x, y);
            page.Canvas.DrawString("Price", font, brush, x + 150, y);
            page.Canvas.DrawString("Quantity", font, brush, x + 250, y);
            page.Canvas.DrawString("Amount", font, brush, x + 350, y);
            y += 20;

            // Draw invoice items
            foreach (var item in invoice.InvoiceItems)
            {
                page.Canvas.DrawString(item.ItemName, font, brush, x, y);
                page.Canvas.DrawString(item.Price.ToString("C"), font, brush, x + 150, y);
                page.Canvas.DrawString(item.Quantity.ToString(), font, brush, x + 250, y);
                page.Canvas.DrawString(item.Amount.ToString("C"), font, brush, x + 350, y);
                y += 20;
            }

            // Draw total amount
            y += 20;
            page.Canvas.DrawString($"Total Amount: {invoice.TotalAmount.ToString("C")}", font, brush, x, y);

            // Save the PDF to a file
            string fileFullPath = AssemblyDirectory.GetFilePath();

            pdf.SaveToFile(fileFullPath);

            // Close the PDF document
            pdf.Close();
        }
    }

    public class Invoice
    {
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
        public decimal TotalAmount => InvoiceItems.Sum(item => item.Amount);
    }

    public class InvoiceItem
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Amount => Price * Quantity;
    }
}