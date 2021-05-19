using API_TEST.Models;
using API_TEST.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_TEST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            return await _productRepository.Get(id);
        }
        [HttpPost]
        public async Task<ActionResult<Product>>PostProducts([FromBody] Product product)
        {
            Product prod = new Product
            {
                Product_Name = product.Product_Name,
                Description = product.Description,
                PurchasePrice = product.PurchasePrice,
                Freight = product.Freight,
                Custom = product.Custom,
                Repair = product.Repair,
                SalePrice = product.SalePrice,
                TotalCost = product.Freight + product.Custom + product.Repair +product.PurchasePrice,
                Profit = product.SalePrice - (product.TotalCost = product.Freight + product.Custom + product.Repair +product.PurchasePrice),

            };
            var newProduct = await _productRepository.Create(prod);
            return CreatedAtAction(nameof(GetProducts), new { id = newProduct.ProductId }, newProduct);
        }
        [HttpPut]

        public async  Task<ActionResult> PutProducts (int id, [FromBody] Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            await _productRepository.Update(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productToDelete = await _productRepository.Get(id);
            if (productToDelete == null)
                return NotFound();
            await _productRepository.Delete(productToDelete.ProductId);
            return NoContent();
            
        }
    }
}
