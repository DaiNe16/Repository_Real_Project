using Repository_Real_Project.Models;

namespace Repository_Real_Project.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByName(string productName);
    }
}
