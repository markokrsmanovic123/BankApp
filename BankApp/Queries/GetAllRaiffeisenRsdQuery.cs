using BankApp.Models;
using MediatR;

namespace BankApp.Queries
{
    public class GetAllRaiffeisenRsdQuery : IRequest<IEnumerable<Transaction>>
    {
    }
}
