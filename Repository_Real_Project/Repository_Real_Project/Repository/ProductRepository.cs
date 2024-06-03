using Microsoft.EntityFrameworkCore;
using Repository_Real_Project.Data;
using Repository_Real_Project.Models;

namespace Repository_Real_Project.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetProductsByName(string productName)
        {
            return await _dbSet.Where(p => p.ProductName == productName).ToListAsync();
        }
    }
}
