namespace BankApp.Repository.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ITransactionRepository RaiffeisenRsdRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
