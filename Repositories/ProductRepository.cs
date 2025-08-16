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
            return product; // todo maybe include foreign keys for debug purposes
        }

        public async Task<Product?> DeleteAsync(Guid productId)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(c => c.Id == productId);
            if (product is not null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return product; // todo maybe include foreign keys for debug purposes
        }

        public async Task<Product?> EditAsync(Product product)
        {
            Product? existingProduct = await _context.Products.FirstOrDefaultAsync(c => c.Id == product.Id);
            if (existingProduct is not null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Code = product.Code;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.Price = product.Price;
                existingProduct.Discount = product.Discount;
                existingProduct.Number = product.Number;
                existingProduct.Displays = product.Displays;
                existingProduct.Details = product.Details;
                existingProduct.Features = product.Features;
                await _context.SaveChangesAsync();
            }
            return existingProduct; // todo maybe include foreign keys for debug purposes
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(e => e.Displays).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.Include(e => e.Displays).Include(e => e.Features).Include(e => e.Details).Include(e => e.Reviews).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
