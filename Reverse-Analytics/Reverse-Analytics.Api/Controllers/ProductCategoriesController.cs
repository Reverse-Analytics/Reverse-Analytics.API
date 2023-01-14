using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reverse_Analytics.Api.Filters;
using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/categories")]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoriesController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetCategories(string? searchQuery)
        {
            var categories = await _productCategoryService.GetProductCategoriesAsync(searchQuery);

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryDto>> GetCategory(int id)
        {
            var category = await _productCategoryService.GetProductCategoryByIdAsync(id);

            if (category is null)
                return NotFound($"Product category with id: {id} does not exist.");

            return Ok(category);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<ProductCategoryDto>> CreateCategoryAsync([FromBody] ProductCategoryForCreateDto categoryToCreate)
        {
            var category = await _productCategoryService.CreateProductCategoryAsync(categoryToCreate);

            if (category is null)
                return StatusCode(500,
                    "Something went wrong while adding product category. Please, try again later.");

            return Created("Category was successfully created.", category);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateCategoryAsync([FromBody] ProductCategoryForUpdateDto categoryToUpdate, int id)
        {
            if (categoryToUpdate.Id != id)
                return BadRequest($"Product Category id: {categoryToUpdate.Id} does not match with route id: {id}.");

            await _productCategoryService.UpdateProductCategoryAsync(categoryToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            await _productCategoryService.DeleteProductCategoryAsync(id);

            return NoContent();
        }
    }
}
