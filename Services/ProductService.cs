using Inventory.Data;
using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetInventoryReportAsync()
        {
            return await _context.Products.ToListAsync();
        }
        // جلب جميع المنتجات
        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // إضافة منتج جديد
        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        // تحديث منتج
        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        // حذف منتج
        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        // جلب منتج حسب المعرف (ID)
        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        // بيع منتج (تقليص الكمية)
        public async Task SellProduct(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null && product.Quantity >= quantity)
            {
                product.Quantity -= quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}
