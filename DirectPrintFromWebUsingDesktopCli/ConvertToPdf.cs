using DirectPrintFromWebUsingDesktopCli;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;

namespace DirectPrint
{
    public static class ConvertToPdf
    {
        public static bool SavePdf(ReceiptModel data, string fullPath)
        {
            try
            {
                DirectDraw(data, fullPath);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong when printing. Message:" + e.Message);
                return false;
            }
        }

        public static bool ConvertAndPrint(ReceiptModel data)
        {
            try
            {
                DirectDraw(data);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong when printing. Message:" + e.Message);
                return false;
            }
        }

        private static void DirectDraw(ReceiptModel receipt, string fullPath = "")
        {
            // Create a new PDF document
            PdfDocument pdf = new PdfDocument();

            // Set the page size to 80 x 210 mm
            PdfUnitConvertor unitConvertor = new PdfUnitConvertor();
            float width = unitConvertor.ConvertUnits(80, PdfGraphicsUnit.Millimeter, PdfGraphicsUnit.Point);
            float height = unitConvertor.ConvertUnits(210, PdfGraphicsUnit.Millimeter, PdfGraphicsUnit.Point);
            PdfPageBase page = pdf.Pages.Add(new SizeF(width, height), new PdfMargins(0));

            // Create a font and style for the header and content
            PdfTrueTypeFont fontHeader = new PdfTrueTypeFont(new Font("Courier New", 15f, FontStyle.Bold));
            PdfTrueTypeFont fontUnderHeader = new PdfTrueTypeFont(new Font("Courier New", 12f, FontStyle.Bold));
            PdfTrueTypeFont fontContent = new PdfTrueTypeFont(new Font("Courier New", 11f, FontStyle.Regular));
            PdfTrueTypeFont fontFooter = new PdfTrueTypeFont(new Font("Courier New", 10f, FontStyle.Italic));

            // Set the starting position for the content
            float x = 6f;
            float y = 0f;

            // Draw the header
            // Draw the centered header
            string headerText = "Bholagonj Toll";
            string headerUnderText = "Sylhet";

            float headerWidth = fontHeader.MeasureString(headerText).Width;
            float headerUnderWidth = fontHeader.MeasureString(headerUnderText).Width;
            float centerX = (width - headerWidth) / 2;
            float centerUnderX = (width - headerUnderWidth) / 2;
            page.Canvas.DrawString(headerText, fontHeader, PdfBrushes.Black, centerX, y);
            y += 15f;
            page.Canvas.DrawString(headerUnderText, fontUnderHeader, PdfBrushes.Black, centerUnderX, y);
            y += 30f;

            // Find the maximum label width for alignment
            float maxLabelWidth = Math.Max(
                fontContent.MeasureString("Invoice ID").Width,
                Math.Max(
                    fontContent.MeasureString("Date & Time").Width,
                    Math.Max(
                        fontContent.MeasureString("Location").Width,
                        Math.Max(
                            fontContent.MeasureString("Counter").Width,
                            Math.Max(
                                fontContent.MeasureString("Collector").Width,
                                Math.Max(
                                    fontContent.MeasureString("Vehicle Type").Width,
                                    Math.Max(
                                        fontContent.MeasureString("Fare").Width,
                                        fontContent.MeasureString("Payment Type").Width
                                    )
                                )
                            )
                        )
                    )
                )
            );

            // Draw the content with aligned colons and maintain the same distance from the left edge
            DrawContentWithAlignedColons(page, fontContent, "Invoice", receipt.Invoice, maxLabelWidth, ref x, ref y);
            DrawContentWithAlignedColons(page, fontContent, "Date & Time", receipt.InvoiceDate.ToString("dd/MM/yyyy hh:mm tt"), maxLabelWidth, ref x, ref y);
            DrawContentWithAlignedColons(page, fontContent, "Location", receipt.Location, maxLabelWidth, ref x, ref y);
            DrawContentWithAlignedColons(page, fontContent, "Station", receipt.Station, maxLabelWidth, ref x, ref y);
            DrawContentWithAlignedColons(page, fontContent, "Operator", receipt.Operator, maxLabelWidth, ref x, ref y);
            DrawContentWithAlignedColons(page, fontContent, "Type", receipt.Type, maxLabelWidth, ref x, ref y);
            DrawContentWithAlignedColons(page, fontContent, "Chargedv", receipt.Charged, maxLabelWidth, ref x, ref y);
            DrawContentWithAlignedColons(page, fontContent, "Payment Type", receipt.PaymentType, maxLabelWidth, ref x, ref y);

            // Draw the centered footer
            const string footerText = "Powered by ...";
            float footerWidth = fontFooter.MeasureString(footerText).Width;
            float footerX = (width - footerWidth) / 2;
            float footerY = y + 40f;
            page.Canvas.DrawString(footerText, fontFooter, PdfBrushes.Black, footerX, footerY);

            if (string.IsNullOrEmpty(fullPath))
            {
                Print(pdf);
            }
            else
            {
                SaveToFolder(pdf, fullPath);
            }
        }

        private static void SaveToFolder(PdfDocument pdf, string fullPath)
        {
            pdf.SaveToFile(fullPath);
            pdf.Close();
        }

        private static void Print(PdfDocument pdf)
        {
            try
            {
                PrintPdf.DirectPrint(pdf);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to print, Message: " + ex.Message);
            }
        }

        private static void DrawContentWithAlignedColons
            (
            PdfPageBase page,
            PdfTrueTypeFont font,
            string label,
            string value,
            float maxLabelWidth,
            ref float x,
            ref float y
            )
        {
            float colonWidth = font.MeasureString(":").Width;

            // Calculate the position to align the colons and content
            float labelWidth = Math.Max(font.MeasureString(label).Width, maxLabelWidth);
            float colonX = x + labelWidth + colonWidth * 0.5f;
            float contentX = colonX + colonWidth * 0.5f;

            // Draw the label, colon, and value
            page.Canvas.DrawString(label, font, PdfBrushes.Black, x, y);
            page.Canvas.DrawString(":", font, PdfBrushes.Black, colonX, y);
            page.Canvas.DrawString(value, font, PdfBrushes.Black, (contentX + 10f), y);

            y += 20f;
        }
    }
}