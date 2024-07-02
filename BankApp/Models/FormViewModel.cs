namespace BankApp.Models
{
    public class FormViewModel
    {
        public required IFormFile FormFile { get; set; }
        public string Bank { get; set; }
        public string Currency { get; set; }
    }
}
