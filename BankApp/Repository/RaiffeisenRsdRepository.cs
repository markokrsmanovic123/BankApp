using BankApp.Data;
using BankApp.Models;
using BankApp.Repository.Interfaces;

namespace BankApp.Repository
{
    public class RaiffeisenRsdRepository(ApplicationDbContext context) : GenericRepository<RaiffeisenRsd>(context), IRaiffeisenRsdRepository
    {
    }
}
