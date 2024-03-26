using AutoMapper;
using FluentAssertions;
using Moq;
using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Services;

namespace ReverseAnalytics.Tests.Unit.Services;

public class ProductCategoryServiceTests
{
    private readonly Mock<ICommonRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ProductCategoryService _productCategoryService;

    public ProductCategoryServiceTests()
    {
        _mockRepository = new Mock<ICommonRepository>();
        _mockMapper = new Mock<IMapper>();
        _productCategoryService = new ProductCategoryService(_mockRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllProductCategories()
    {
        // Arrange
        var categories = new List<ProductCategory>(); // Add sample categories
        _mockRepository.Setup(r => r.ProductCategory.FindAllAsync()).ReturnsAsync(categories);

        var categoryDtos = new List<ProductCategoryDto>(); // Add corresponding DTOs
        _mockMapper.Setup(m => m.Map<IEnumerable<ProductCategoryDto>>(categories)).Returns(categoryDtos);

        // Act
        var result = await _productCategoryService.GetAllAsync();

        // Assert
        categoryDtos.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldHandleEmptyResultSet()
    {
        // Arrange
        _mockRepository.Setup(r => r.ProductCategory.FindAllAsync()).ReturnsAsync(new List<ProductCategory>());

        // Act
        var result = await _productCategoryService.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ShouldHandleMappingFailure()
    {
        // Arrange
        var categories = new List<ProductCategory>(); // Add sample categories
        _mockRepository.Setup(r => r.ProductCategory.FindAllAsync()).ReturnsAsync(categories);

        _mockMapper.Setup(m => m.Map<IEnumerable<ProductCategoryDto>>(categories)).Throws(new Exception("Mapping failed"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _productCategoryService.GetAllAsync());
    }
}