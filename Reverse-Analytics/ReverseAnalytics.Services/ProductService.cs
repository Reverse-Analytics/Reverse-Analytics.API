using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Product;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class ProductService : IProductService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>?> GetProductsAsync()
        {
            try
            {
                var products = await _repository.Product.FindAllAsync();

                if (products is null)
                {
                    return null;
                }

                var productDtos = _mapper.Map<IEnumerable<ProductDto>?>(products);

                if (productDtos is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(Product)} Entities to {typeof(ProductDto)}.");
                }

                return productDtos;
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
                throw new Exception("There was an error retrieving Products.", ex);
            }
        }

        public async Task<IEnumerable<ProductDto>?> GetProductsAsync(string? searchString)
        {
            try
            {
                var products = await _repository.Product.FindAllProductsAsync(searchString);

                if (products is null)
                {
                    return null;
                }

                var productDtos = _mapper.Map<IEnumerable<ProductDto>?>(products);

                if (productDtos is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(Product)} Entities to {typeof(ProductDto)}.");
                }

                return productDtos;
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

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _repository.Product.FindByIdAsync(id);

                if(product is null)
                {
                    return null;
                }

                var productDto = _mapper.Map<ProductDto>(product);

                if (productDto is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(Product)} Entity to {typeof(ProductDto)}.");
                }

                return productDto;
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

        public async Task<ProductDto> CreateProductAsync(ProductForCreateDto productToCreate)
        {
            try
            {
                if(productToCreate is null)
                {
                    throw new ArgumentNullException(nameof(productToCreate));
                }

                var productEntity = _mapper.Map<Product>(productToCreate);

                if(productEntity is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(ProductForCreateDto)} to {typeof(Product)} Entity.");
                }

                productEntity = _repository.Product.Create(productEntity);
                await _repository.Product.SaveChangesAsync();

                var productDto = _mapper.Map<ProductDto>(productEntity);

                if(productDto is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(Product)} Entity to {typeof(ProductDto)}.");
                }

                return productDto;
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
                throw new Exception("There was an error while adding new Product.", ex);
            }
        }

        public async Task UpdateProductAsync(ProductForUpdateDto productToUpdate)
        {
            try
            {
                if(productToUpdate is null)
                {
                    throw new ArgumentNullException(nameof(productToUpdate));
                }

                var productEntity = _mapper.Map<Product>(productToUpdate);

                if(productEntity is null)
                {
                    throw new AutoMapperMappingException($"Could not map {typeof(ProductForUpdateDto)} to {typeof(Product)} Entity.");
                }

                _repository.Product.Update(productEntity);
                await _repository.Product.SaveChangesAsync();
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
                throw new Exception("There was an error while updating Product.", ex);
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            try
            {
                _repository.Product.Delete(id);
                await _repository.Product.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception($"There was an error deleting Product with id: {id}.", ex);
            }
        }
    }
}
