using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ILogger<ProductCategoryController> _logger;
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(ILogger<ProductCategoryController> logger, IProductCategoryService productCategoryService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
            _productCategoryService = productCategoryService;
        }

        #region Actions

        // GET: api/<ProductCategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetCategories(string? searchQuery)
        {
            try
            {
                var categories = await _productCategoryService.GetAllProductCategoriesAsync(searchQuery);

                if (categories is null || !categories.Any())
                {
                    return NotFound("No categories were found.");
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error retrieving product categories.", ex.Message);

                return StatusCode(500, "There was an error retrieving product categories.");
            }
        }

        // GET api/<ProductCategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryDto>> GetCategory(int id)
        {
            try
            {
                var category = await _productCategoryService.GetProductCategoryByIdAsync(id);

                if (category is null)
                {
                    return NotFound($"There is no product category with id: {id}.");
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving product category with id: {id}.", ex.Message);

                return StatusCode(500, $"There was an error retrieving product category with id: {id}.");
            }
        }

        // POST api/<ProductCategoryController>
        [HttpPost]
        public async Task<ActionResult<ProductCategoryDto>> Post(ProductCategoryForCreateDto categoryToCreate)
        {
            try
            {
                if (categoryToCreate is null)
                {
                    return BadRequest("Product category cannot be null.");
                }

                var category = await _productCategoryService.CreateProductCategoryAsync(categoryToCreate);

                if (category is null)
                {
                    return StatusCode(500, "Unknown error has occured while creating new product category. Please, try again later.");
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while creating new category.", ex.Message);

                return StatusCode(500, "There was an error while creating new product category.");
            }
        }

        // PUT api/<ProductCategoryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductCategoryForUpdateDto>> Put(int id, ProductCategoryForUpdateDto categoryToUpdate)
        {
            try
            {
                if (categoryToUpdate is null)
                {
                    return BadRequest("Catogory cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Product category object is invalid for updating.");
                }

                if (categoryToUpdate.Id != id)
                {
                    return BadRequest($"Category id: {categoryToUpdate.Id} does not match with route id: {id}.");
                }

                await _productCategoryService.UpdateProductCategoryAsync(categoryToUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating product category with id: {id}.", ex.Message);

                return StatusCode(500, $"There was an error updating product category with id: {id}.");
            }
        }

        // DELETE api/<ProductCategoryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _productCategoryService.DeleteProductCategoryAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting category with id: {id}.", ex.Message);

                return StatusCode(500, $"There was an error while deleting product category with id: {id}.");
            }
        }

        #endregion
    }
}
