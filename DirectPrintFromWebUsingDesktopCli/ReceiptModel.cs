using System.ComponentModel.DataAnnotations;

namespace DirectPrintFromWebUsingDesktopCli
{
    public class ReceiptModel
    {
        [Display(Name = "Date & Time")]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Class")]
        public string Class { get; set; } = string.Empty;

        [Display(Name = "Invoice")]
        public string Invoice { get; set; } = string.Empty;

        [Display(Name = "Location")]
        public string Location { get; set; } = string.Empty;

        [Display(Name = "Station")]
        public string Station { get; set; } = string.Empty;

        [Display(Name = "Operator")]
        public string Operator { get; set; } = string.Empty;

        [Display(Name = "Type")]
        public string Type { get; set; } = string.Empty;

        [Display(Name = "Charged")]
        public string Charged { get; set; } = string.Empty;

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}