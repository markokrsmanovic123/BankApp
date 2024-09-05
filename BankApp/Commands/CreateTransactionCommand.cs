using MediatR;

namespace BankApp.Commands
{
    public class CreateTransactionCommand : IRequest<Unit>
    {
        public IEnumerable<IFormFile> FormFiles { get; set; }
        public string Bank { get; set; }
        public string Currency { get; set; }

        public CreateTransactionCommand(IEnumerable<IFormFile> formFiles, string bank, string currency)
        {
            FormFiles = formFiles;
            Bank = bank;
            Currency = currency;
        }
    }
}
