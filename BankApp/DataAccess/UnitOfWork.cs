using BankApp.Data;
using BankApp.Repository;
using BankApp.Repository.Interfaces;

namespace BankApp.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            RaiffeisenRsdRepository = new RaiffeisenRsdRepository(_context);
        }

        public IRaiffeisenRsdRepository RaiffeisenRsdRepository { get; private set; }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
