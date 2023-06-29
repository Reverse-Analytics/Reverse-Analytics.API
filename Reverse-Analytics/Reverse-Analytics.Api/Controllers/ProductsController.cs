using Microsoft.AspNetCore.Mvc;
using Reverse_Analytics.Api.Filters;
using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/Products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        private const int PageSize = 15;
        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductsAsync(string? searchString, int? categoryId, int pageSize = PageSize, int pageNumber = 1)
        {
            var products = await _service.GetProductsAsync(searchString, categoryId, pageSize, pageNumber);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductByIdAsync(int id)
        {
            var product = await _service.GetProductByIdAsync(id);

            if (product is null)
                return NotFound($"Product with id: {id} does not exist.");

            return Ok(product);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<ProductDto>> CreateProductAsync([FromBody] ProductForCreateDto productToCreate)
        {
            var product = await _service.CreateProductAsync(productToCreate);

            if (product is null)
                return StatusCode(500,
                    "Something went wrong while creating new Product. Please, try again later.");

            return Created("Product was sucessfully created.", product);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateProductAsync([FromBody] ProductForUpdateDto productToUpdate, int id)
        {
            if (productToUpdate.Id != id)
                return BadRequest($"Product id: {productToUpdate.Id} does not match with route id: {id}.");

            await _service.UpdateProductAsync(productToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductAsync(int id)
        {
            await _service.DeleteProductAsync(id);

            return NoContent();
        }
    }
}
