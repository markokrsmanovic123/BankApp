using BankApp.Models;
using BankApp.Queries;
using BankApp.Repository.Interfaces;
using MediatR;

namespace BankApp.Handlers
{
    public class GetAllRaiffeisenRsdQueryHandler : IRequestHandler<GetAllRaiffeisenRsdQuery, IEnumerable<Transaction>>
    {
        public IUnitOfWork _unitOfWork { get; private set; }

        public GetAllRaiffeisenRsdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Transaction>> Handle(GetAllRaiffeisenRsdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.RaiffeisenRsdRepository.GetAllAsync();
        }
    }
}
