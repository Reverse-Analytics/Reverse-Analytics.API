using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Sale;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class SaleService : ISaleService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public SaleService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SaleDto>> GetAllSalesAsync(int pageSize, int pageNumber)
        {
            try
            {
                var sales = await _repository.Sale.FindAllAsync(pageSize, pageNumber);

                var saleDtos = _mapper.Map<IEnumerable<SaleDto>>(sales);

                return saleDtos;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving Sales.", ex);
            }
        }

        public async Task<SaleDto> GetSaleByIdAsync(int id)
        {
            try
            {
                var sale = await _repository.Sale.FindByIdAsync(id);

                var saleDto = _mapper.Map<SaleDto>(sale);

                return saleDto;
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
                throw new Exception($"Error retrieving Sale with id: {id}", ex);
            }
        }

        public async Task<SaleDto> CreateSaleAsync(SaleForCreateDto saleToCreate)
        {
            try
            {
                var saleEntity = _mapper.Map<Sale>(saleToCreate);
                var saleDetails = _mapper.Map<ICollection<SaleDetail>>(saleToCreate?.SaleDetails);

                saleEntity = _repository.Sale.Create(saleEntity);
                saleEntity.Receipt = Guid.NewGuid().ToString()[..8];

                if (saleDetails != null && saleDetails.Any())
                {
                    _repository.SaleDetail.CreateRange(saleDetails);
                }

                await _repository.SaveChangesAsync();

                var saleDto = _mapper.Map<SaleDto>(saleEntity);

                return saleDto;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error creating new Sale.", ex);
            }
        }

        public async Task UpdateSaleAsync(SaleForUpdateDto saleToUpdate)
        {
            try
            {
                var saleEntity = _mapper.Map<Sale>(saleToUpdate);

                _repository.Sale.Update(saleEntity);
                await _repository.Sale.SaveChangesAsync();
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
                throw new Exception($"There was an error updating Sale with id: {saleToUpdate?.Id}.", ex);
            }
        }

        public async Task DeleteSaleAsync(int id)
        {
            try
            {
                _repository.Sale.Delete(id);
                await _repository.Sale.SaveChangesAsync();
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error deleting Sale with id: {id}.", ex);
            }
        }
    }
}