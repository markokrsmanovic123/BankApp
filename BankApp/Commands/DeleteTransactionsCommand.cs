using MediatR;

namespace BankApp.Commands
{
    public class DeleteTransactionsCommand : IRequest<Unit>
    {
        public List<int> Ids { get; set; }
        
        public DeleteTransactionsCommand(List<int> ids)
        {
            Ids = ids;
        }
    }
}
