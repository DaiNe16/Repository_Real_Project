namespace Repository_Real_Project.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        Task<int> SaveChangeAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollBackAsync();

        TRepository GetRepository<TRepository, TEntity>()
            where TRepository : class, IRepository<TEntity>
            where TEntity : class;
    }
}
