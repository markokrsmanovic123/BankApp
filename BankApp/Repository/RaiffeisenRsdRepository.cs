using BankApp.Data;
using BankApp.Models;
using BankApp.Repository.Interfaces;

namespace BankApp.Repository
{
    public class RaiffeisenRsdRepository : GenericRepository<RaiffeisenRsd>, IRaiffeisenRsdRepository
    {
        public RaiffeisenRsdRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
