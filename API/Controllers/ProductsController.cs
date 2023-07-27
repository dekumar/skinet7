using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase {

        // here we are directly intracting with context
        // private readonly StoreContext _context;
        // public ProductsController(StoreContext context) {
        //     _context = context;
        // }
        private readonly IProductRepository _repository;

        //usign the repository to access database context
        public ProductsController(IProductRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts() {
            //var products = await _context.Products.ToListAsync();

            //changing the method to access data via repository
            var products = await _repository.GetProductsAsync();
            return Ok(products);
            //return "this will return list of products";
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) {
            //var product = await _context.Products.FindAsync(id);

            //updating method to access the repository 
            var product = await _repository.GetProductByIdAsync(id);
            return product;
            //return "this will be a product";
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() {
            return Ok(await _repository.GetProductBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes() {
            return Ok(await _repository.GetProductTypesAsync());
        }
    }
}