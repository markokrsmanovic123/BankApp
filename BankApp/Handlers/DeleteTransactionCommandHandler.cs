using BankApp.Commands;
using BankApp.Repository.Interfaces;
using MediatR;

namespace BankApp.Handlers
{
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionsCommand, Unit>
    {
        public IUnitOfWork UnitOfWork { get; private set; }

        public DeleteTransactionCommandHandler(IUnitOfWork unitOfWork) 
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteTransactionsCommand command, CancellationToken cancellationToken)
        {
            if (command.Ids.Any()) 
            { 
                foreach (var id in command.Ids) 
                {
                    var transaction = await UnitOfWork.RaiffeisenRsdRepository.GetByIdAsync(id);
                    UnitOfWork.RaiffeisenRsdRepository.Remove(transaction);
                }
            await UnitOfWork.SaveChangesAsync();
            }

            return Unit.Value;
        }

    }
}
