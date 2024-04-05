namespace BankApp.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRaiffeisenRsdRepository RaiffeisenRsdRepository { get; }
        int Save();
    }
}
