using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/categories")]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly ILogger<ProductCategoriesController> _logger;
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoriesController(ILogger<ProductCategoriesController> logger, IProductCategoryService productCategoryService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
            _productCategoryService = productCategoryService;
        }

        #region Actions

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetCategories(string? searchQuery)
        {
            try
            {
                var categories = await _productCategoryService.GetProductCategoriesAsync(searchQuery);

                if (categories is null)
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

        [HttpPost]
        public async Task<ActionResult<ProductCategoryDto>> Post(ProductCategoryForCreateDto categoryToCreate)
        {
            try
            {
                if (categoryToCreate is null)
                {
                    return BadRequest("Product category cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Product category is not valid for creation.");
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ProductCategoryForUpdateDto categoryToUpdate)
        {
            try
            {
                if (categoryToUpdate is null)
                {
                    return BadRequest("Product Catogory cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Product Category is not valid for updating.");
                }

                if (categoryToUpdate.Id != id)
                {
                    return BadRequest($"Product Category id: {categoryToUpdate.Id} does not match with route id: {id}.");
                }

                await _productCategoryService.UpdateProductCategoryAsync(categoryToUpdate);

                return NoContent();
            }
            catch(NotFoundException ex)
            {
                _logger.LogError($"Unable to find Product Category with id: {id}.", ex.Message);

                return NotFound($"Product category with id: {id} does not exist.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating product category with id: {id}.", ex.Message);

                return StatusCode(500, $"Unkown error has occured while updating the Product Category.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _productCategoryService.DeleteProductCategoryAsync(id);

                return NoContent();
            }
            catch(NotFoundException ex)
            {
                _logger.LogError($"Error deleting Product Category with id: {id}.", ex.Message);

                return NotFound($"Product category with id: {id} does not exist.");
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
