using BankApp.Models;
using MediatR;

namespace BankApp.Queries
{
    public class GetAllTransactionsQuery : IRequest<IEnumerable<Transaction>>
    {
    }
}
