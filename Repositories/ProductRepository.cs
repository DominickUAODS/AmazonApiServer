using AmazonApiServer.Data;
using AmazonApiServer.Interfaces;
using AmazonApiServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonApiServer.Repositories
{
    public class ProductRepository(ApplicationContext context) : IProductRepo
    {
        private readonly ApplicationContext _context = context;

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteAsync(Guid productId)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(c => c.Id == productId);
            if (product is not null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return product;
        }

        public async Task<Product?> EditAsync(Product product)
        {
            Product? existingProduct = await _context.Products.FirstOrDefaultAsync(c => c.Id == product.Id);
            if (existingProduct is not null)
            {
                existingProduct.Code = product.Code;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.Price = product.Price;
                existingProduct.Discount = product.Discount;
                existingProduct.Number = product.Number;
                await _context.SaveChangesAsync();
            }
            return existingProduct;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
