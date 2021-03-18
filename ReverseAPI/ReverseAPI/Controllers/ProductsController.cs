using Microsoft.AspNetCore.Mvc;
using ReverseAPI.DAL;
using ReverseAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReverseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {

        private readonly IDbLayer _context;
        public ProductsController(IDbLayer context)
        {
            _context = context;
        }

        // GET: ProductsController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            try
            {
                var products = await _context.GetProducts();

                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: ProductsController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _context.GetProduct(id);

            int g = 0;

            if (product == null) return NotFound();

            return Ok(product);
        }

        // GET: ProductsController/Create
        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product newProduct)
        {
            await _context.AddProduct(newProduct);

            int g = 0;

            return CreatedAtAction(nameof(Get), new { id = newProduct.ProductId }, newProduct);
        }

        // GET: ProductsController/Edit/5
        [HttpPut("{id}")]

        public async Task<ActionResult> Update(Product productToUpdate)
        {
            int g = 0;

            var product = await _context.GetProduct(productToUpdate.ProductId);

            if (product == null) return NotFound();

            product.ProductName = productToUpdate.ProductName;
            product.SalePrice = productToUpdate.SalePrice;

            await _context.UpdateProduct(product);

            return NoContent();
        }

        // GET: ProductsController/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _context.GetProduct(id);

            int g = 0;

            if (product == null) return NotFound();

            await _context.DeleteProduct(id);

            return NoContent();
        }
    }
}
