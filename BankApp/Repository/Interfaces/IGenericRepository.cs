namespace BankApp.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
    }
}
