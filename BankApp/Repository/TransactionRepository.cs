using BankApp.Data;
using BankApp.Models;
using BankApp.Repository.Interfaces;

namespace BankApp.Repository
{
    public class TransactionRepository(ApplicationDbContext context) : GenericRepository<Transaction>(context), ITransactionRepository
    {
    }
}
