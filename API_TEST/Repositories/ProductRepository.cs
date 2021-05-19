using System.Collections.Generic;
using System.Threading.Tasks;
using API_TEST.Data;
using API_TEST.Models;
using Microsoft.EntityFrameworkCore;

namespace API_TEST.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Product> Create (Product product)
        {
            //Product prod = new Product {
            //    Product_Name=product.Product_Name,
            //    Freight=product.Freight,
            //    Custom = product.Custom,
            //    Repair = product.Repair,
            //    PriceSell = product.PriceSell,
            //    PricePurchase = product.Freight+ product.Custom+ product.Repair,

            //};
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task Delete(int id)
        {
            var productToDelete = await _context.Products.FindAsync(id);
            _context.Products.Remove(productToDelete);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Product>> Get()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> Get(int id)
        {
            return await _context.Products.FirstAsync();
        }

        public async Task Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}