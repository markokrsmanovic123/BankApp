using BankApp.Models;
using BankApp.Queries;
using BankApp.Repository.Interfaces;
using MediatR;

namespace BankApp.Handlers
{
    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, IEnumerable<Transaction>>
    {
        public IUnitOfWork _unitOfWork { get; private set; }

        public GetAllTransactionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Transaction>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.RaiffeisenRsdRepository.GetAllAsync();
        }
    }
}
