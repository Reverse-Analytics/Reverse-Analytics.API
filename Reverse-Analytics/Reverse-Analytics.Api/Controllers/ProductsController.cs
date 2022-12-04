using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        const int pageSize = 20;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync(string? searchString, int? categoryId, int pageSize = pageSize, int pageNumber = 1)
        {
            var products = await _service.GetProductsAsync(searchString, categoryId, pageSize, pageNumber);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(int id)
        {
            var product = await _service.GetProductByIdAsync(id);

            if (product is null)
                return NotFound($"Product with id: {id} does not exist.");

            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Post(ProductForCreateDto productToCreate)
        {
            try
            {
                if(productToCreate is null)
                {
                    return BadRequest("Product cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Product is not valid for creation.");
                }

                var product = await _service.CreateProductAsync(productToCreate);

                if(product is null)
                {
                    return StatusCode(500, "Unknown error has occured while creating new product.");
                }

                return Ok(product);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while creating a new product.", ex.Message);

                return StatusCode(500, "There was an error creating a new product.");
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ProductForUpdateDto productToUpdate)
        {
            try
            {
                if (productToUpdate is null)
                {
                    return BadRequest("Product cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Product is not valid for updating.");
                }

                if (productToUpdate.Id != id)
                {
                    return BadRequest($"Product id: {productToUpdate.Id} does not match with route id: {id}.");
                }

                await _service.UpdateProductAsync(productToUpdate);
                
                return NoContent();
            }
            catch(NotFoundException ex)
            {
                _logger.LogError($"Unable to find product with id: {id} to update.", ex.Message);

                return NotFound($"Product with id: {id} does not exist.");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error updating product with id: {id}.", ex.Message);

                return StatusCode(500, "Unknown error has occured while updating the product.");
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteProductAsync(id);
                
                return NoContent();
            }
            catch(NotFoundException ex)
            {
                _logger.LogError($"Unable to find product with id: {id} to delete.", ex.Message);

                return NotFound($"Product with id: {id} does not exist.");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error deleting product with id: {id}.", ex.Message);

                return StatusCode(500, "Unknown error has occured while deleting the product.");
            }
        }
    }
}
