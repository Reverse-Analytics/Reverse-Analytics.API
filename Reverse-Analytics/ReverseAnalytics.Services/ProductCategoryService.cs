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
        private readonly IProductCategoryRepository _repository;
        private readonly IMapper _mapper;

        public ProductCategoryService(IProductCategoryRepository repository, IMapper mapper)
        {
            _repository = repository
                ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetAllProductCategoriesAsync(string? searchString)
        {
            try
            {
                var productCategories = await _repository.FindAllProductCategoriesAsync(searchString);

                if (productCategories is null)
                    throw new NotFoundException("No product categories found.");

                var productCategoriesDto = _mapper.Map<IEnumerable<ProductCategoryDto>>(productCategories);

                if (productCategoriesDto is null)
                    throw new AutoMapperMappingException("Could not map ProductCategory Entities to ProductCategory DTOs.");

                return productCategoriesDto;
            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception("There was an error retrieving product categories.", ex);
            }
        }

        public async Task<ProductCategoryDto?> GetProductCategoryByIdAsync(int id)
        {
            try
            {
                var productCategory = await _repository.FindProductCategoryByIdAsync(id);

                if (productCategory is null)
                    throw new NotFoundException($"Could not find ProductCategory with id: {id}");

                var productCategoryDto = _mapper.Map<ProductCategoryDto>(productCategory);

                if (productCategoryDto is null)
                    throw new AutoMapperMappingException("Could not map ProductCategory Entity to ProductCategory DTO.");

                return productCategoryDto;
            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch(AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception($"There was an error retrieving Product Category with id: {id}", ex);
            }
        }

        public async Task<ProductCategoryDto?> CreateProductCategoryAsync(ProductCategoryForCreateDto productCategoryToCreate)
        {
            try
            {
                if (productCategoryToCreate is null)
                    throw new ArgumentNullException(nameof(productCategoryToCreate));

                var productCategoryEntity = _mapper.Map<ProductCategory>(productCategoryToCreate);

                if (productCategoryEntity is null)
                    throw new AutoMapperMappingException("There was an error mapping ProductCategoryForCreate DTO to ProductCategory Entity.");

                _repository.Create(productCategoryEntity);
                await _repository.SaveChangesAsync();

                var productCategoryDto = _mapper.Map<ProductCategoryDto>(productCategoryEntity);

                if(productCategoryDto is null)
                    throw new AutoMapperMappingException("There was an error mapping ProductCategory Entity to  ProductCategoryForCreate DTO.");

                return productCategoryDto;
            }
            catch(ArgumentNullException ex)
            {
                throw ex;
            }
            catch(AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception("There was an error while adding new Product Category.", ex);
            }
        }

        public async Task UpdateProductCategoryAsync(ProductCategoryForUpdateDto productCategoryToUpdate)
        {
            try
            {
                if (productCategoryToUpdate is null)
                    throw new ArgumentNullException(nameof(productCategoryToUpdate));

                var productCategoryEntity = _mapper.Map<ProductCategory>(productCategoryToUpdate);

                if (productCategoryEntity is null)
                    throw new AutoMapperMappingException("There was an error mapping ProductCategoryForUpdate DTO to Product Category Entity.");

                _repository.Update(productCategoryEntity);
                await _repository.SaveChangesAsync();
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
                throw new Exception("There was an error updating Product Category", ex);
            }
        }

        public async Task DeleteProductCategoryAsync(int productCategoryId)
        {
            try
            {
                var productCategoryEntity = _mapper.Map<ProductCategory>(productCategoryId);

                if (productCategoryEntity is null)
                    throw new NotFoundException($"Could not find Product Category with id: {productCategoryId}");

                _repository.Delete(productCategoryEntity);
                await _repository.SaveChangesAsync();
            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception($"There was an error deleting Product Category with id: {productCategoryId}", ex);
            }
        }
    }
}
