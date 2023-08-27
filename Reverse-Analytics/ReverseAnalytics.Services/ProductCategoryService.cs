using AutoMapper;

using ReverseAnalytics.Domain.DTOs.ProductCategory;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public ProductCategoryService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetProductCategoriesAsync()
        {
            try
            {
                var productCategories = await _repository.ProductCategory.FindAllAsync();

                if (productCategories is null)
                {
                    return null;
                }

                var productCategoriesDto = _mapper.Map<IEnumerable<ProductCategoryDto>>(productCategories);

                foreach (var category in productCategoriesDto)
                {
                    category.NumberOfProducts = category.Products.Count;
                }

                return productCategoriesDto;
            }
            catch (NotFoundException ex)
            {
                throw;
            }
            catch (AutoMapperMappingException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error retrieving product categories.", ex);
            }
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetProductCategoriesAsync(string? searchString)
        {
            try
            {
                var productCategories = await _repository.ProductCategory.FindAllProductCategoriesAsync(searchString);

                if (productCategories is null)
                    return Enumerable.Empty<ProductCategoryDto>();

                var productCategoriesDto = _mapper.Map<IEnumerable<ProductCategoryDto>>(productCategories);

                foreach (var category in productCategoriesDto)
                {
                    category.NumberOfProducts = category.Products.Count;
                }

                return productCategoriesDto;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error retrieving product categories.", ex);
            }
        }

        public async Task<ProductCategoryDto?> GetProductCategoryByIdAsync(int id)
        {
            try
            {
                var productCategory = await _repository.ProductCategory.FindByIdAsync(id);

                if (productCategory is null)
                {
                    return null;
                }

                var productCategoryDto = _mapper.Map<ProductCategoryDto>(productCategory);

                if (productCategoryDto is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(ProductCategory)} Entity to {typeof(ProductCategoryDto)}.");
                }

                return productCategoryDto;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error retrieving Product Category with id: {id}", ex);
            }
        }

        public async Task<ProductCategoryDto?> CreateProductCategoryAsync(ProductCategoryForCreateDto productCategoryToCreate)
        {
            try
            {
                if (productCategoryToCreate is null)
                {
                    throw new ArgumentNullException(nameof(productCategoryToCreate));
                }

                var productCategoryEntity = _mapper.Map<ProductCategory>(productCategoryToCreate);

                if (productCategoryEntity is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(ProductCategoryForCreateDto)} to {typeof(ProductCategory)}.");
                }

                _repository.ProductCategory.Create(productCategoryEntity);
                await _repository.ProductCategory.SaveChangesAsync();

                var productCategoryDto = _mapper.Map<ProductCategoryDto>(productCategoryEntity);

                if (productCategoryDto is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(ProductCategory)} Entity to {typeof(ProductCategoryDto)}.");
                }

                return productCategoryDto;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error while adding new Product Category.", ex);
            }
        }

        public async Task UpdateProductCategoryAsync(ProductCategoryForUpdateDto productCategoryToUpdate)
        {
            try
            {
                if (productCategoryToUpdate is null)
                {
                    throw new ArgumentNullException(nameof(productCategoryToUpdate));
                }

                var productCategoryEntity = _mapper.Map<ProductCategory>(productCategoryToUpdate);

                if (productCategoryEntity is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(ProductCategory)} Entity to {typeof(ProductCategoryDto)}.");
                }

                _repository.ProductCategory.Update(productCategoryEntity);
                await _repository.ProductCategory.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error updating Product Category", ex);
            }
        }

        public async Task DeleteProductCategoryAsync(int productCategoryId)
        {
            try
            {
                _repository.ProductCategory.Delete(productCategoryId);
                await _repository.ProductCategory.SaveChangesAsync();
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error deleting Product Category with id: {productCategoryId}", ex);
            }
        }
    }
}
