using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository_Real_Project.Data;
using Repository_Real_Project.Models;
using Repository_Real_Project.Repository;

namespace Repository_Real_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IRepository<Product> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
          var products = await _unitOfWork.GetRepository<Product>().GetAllAsync();
            return Ok(products);
        }
        // GET: api/Products
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(id);
            return Ok(product);
        }
        // GET: api/Products
        [HttpGet("GetProductByName")]
        public async Task<ActionResult<Product>> GetProductByName(string name)
        {
            var product = await _unitOfWork.GetRepository<IProductRepository, Product>().GetProductsByName(name);
            return Ok(product);
        }
        // GET: api/Products
        [HttpPost]
        public async Task<Product> AddProduct(Product product)
        {
           return await _repository.AddAsync(product);
        }

    }
}
