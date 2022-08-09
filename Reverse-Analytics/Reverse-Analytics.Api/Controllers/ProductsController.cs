using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/Products")]
    [ApiController] 
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger, IProductService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? searchString)
        {
            try
            {
                var products = await _service.GetProductsAsync(searchString);

                if (products is null)
                {
                    return NotFound("No products were found.");
                }

                return Ok(products);
            }
            catch(Exception ex)
            {
                _logger.LogError(500, "Error retrieving products.", ex.Message);

                return StatusCode(500, "There was an error retrieving products.");
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _service.GetProductByIdAsync(id);

                if(product is null)
                {
                    return NotFound($"There is no product with id: {id}.");
                }

                return Ok(product);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while retrieving product with id: {id}", ex.Message);

                return StatusCode(500, $"There was an error retrieving product with id: {id}.");
            }
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
