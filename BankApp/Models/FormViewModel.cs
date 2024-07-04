using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace BankApp.Models
{
    public class FormViewModel
    {
        [Required(ErrorMessage = "Fajl je obavezan!")]
        public IFormFile FormFile { get; set; }

        [RegularExpression("^(raiffeisen|erste)$", ErrorMessage = "Vrednost nije validna!")]
        public string Bank { get; set; }

        [RegularExpression("^(rsd|eur|usd)$", ErrorMessage = "Vrednost nije validna!")]
        public string Currency { get; set; }
        public IEnumerable<RaiffeisenRsd> RaiffeisenRsd { get; set; }
    }
}
