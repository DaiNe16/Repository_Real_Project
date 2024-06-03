
using Microsoft.EntityFrameworkCore.Storage;
using Repository_Real_Project.Data;

namespace Repository_Real_Project.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<Type, object> _repositories;

        private IDbContextTransaction _transaction;

        public UnitOfWork(AppDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _repositories = new Dictionary<Type, object>();
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch 
            {
                await _transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null!;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if(disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }


        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T))) 
            {
                return _repositories[typeof(T)] as IRepository<T>;
            }
            var repository = new Repository<T>(_context);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        public async Task RollBackAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }

        TRepository IUnitOfWork.GetRepository<TRepository, TEntity>()
        {
            var repository = _serviceProvider.GetService<TRepository>();
            if(repository == null)
            {
                throw new InvalidOperationException($"Failed to get repository type {typeof(TRepository)}");
            }
            //Set the DbContext
            if(repository is IRepository<TEntity>)
            {
                repository.SetDbContext(_context);
            }
            else
            {
                throw new InvalidOperationException($"Repository of type {typeof(TRepository)} does not match with entity.");
            }
            return repository;
        }
    }
}
