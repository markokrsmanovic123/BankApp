using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

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

        [ValidateNever]
        public IEnumerable<RaiffeisenRsd> RaiffeisenRsd { get; set; }
    }
}
