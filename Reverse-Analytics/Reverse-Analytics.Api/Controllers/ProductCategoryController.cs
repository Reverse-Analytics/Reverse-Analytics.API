using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.Interfaces.Services;
using ReverseAnalytics.Domain.QueryParameters;

namespace Reverse_Analytics.Api.Controllers;

[Route("api/categories")]
[ApiController]
public class ProductCategoryController(IProductService productService, IProductCategoryService productCategoryService) : ControllerBase
{
    private readonly IProductService _productService = productService;
    private readonly IProductCategoryService _productCategoryService = productCategoryService;

    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProductCategoryDto>>> GetAsync([FromQuery] ProductCategoryQueryParameters queryParameters)
    {
        var categories = await _productCategoryService.GetAsync(queryParameters);

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductCategoryDto>> GetAsync(int id)
    {
        var category = await _productCategoryService.GetByIdAsync(id);

        return Ok(category);
    }

    [HttpGet("{id}/products")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsAsync(int id)
    {
        var products = await _productService.GetByCategoryAsync(id);

        return Ok(products);
    }

    [HttpGet("{id}/children")]
    public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetChildrenAsync(int id)
    {
        var subCategories = await _productCategoryService.GetAllByParentIdAsync(id);

        return Ok(subCategories);
    }

    [HttpPost]
    public async Task<ActionResult<ProductCategoryDto>> CreateAsync(ProductCategoryForCreateDto categoryToCreate)
    {
        var createdCategory = await _productCategoryService.CreateAsync(categoryToCreate);

        return Ok(createdCategory);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductCategoryDto>> UpdateAsync(int id, [FromBody] ProductCategoryForUpdateDto categoryToUpdate)
    {
        if (id != categoryToUpdate.Id)
        {
            return BadRequest($"Route id: {id} does not match with category id: {categoryToUpdate.Id}");
        }

        var updatedCategory = await _productCategoryService.UpdateAsync(categoryToUpdate);

        return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _productCategoryService.DeleteAsync(id);

        return NoContent();
    }
}
