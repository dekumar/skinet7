using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data {
    public class ProductRepository : IProductRepository {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context) {
            _context=context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToArrayAsync();
            //throw new NotImplementedException();
        }

        public async Task<Product> GetProductByIdAsync(int id) {
            return await _context.Products
                .Include(p=>p.ProductBrand)
                .Include(P=>P.ProductType)
                .FirstOrDefaultAsync(p=>p.Id==id);
            //throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync() {
            return await _context.Products
                .Include(p=>p.ProductBrand)
                .Include(P=>P.ProductType)
                .ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
            //throw new NotImplementedException();
        }
    }
}