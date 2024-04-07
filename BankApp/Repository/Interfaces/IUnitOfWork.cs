namespace BankApp.Repository.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRaiffeisenRsdRepository RaiffeisenRsdRepository { get; }
        Task<int> Save();
    }
}
