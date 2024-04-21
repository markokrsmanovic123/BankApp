using MediatR;

namespace BankApp.Commands
{
    public class CreateTransactionCommand : IRequest<Unit>
    {
        public IFormFile FormFile { get; set; }
        public string Bank { get; set; }
        public string Currency { get; set; }

        public CreateTransactionCommand(IFormFile formFile, string bank, string currency)
        {
            FormFile = formFile;
            Bank = bank;
            Currency = currency;
        }
    }
}
