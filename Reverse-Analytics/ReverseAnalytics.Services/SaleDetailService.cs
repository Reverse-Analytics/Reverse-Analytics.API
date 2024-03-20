using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SaleItem;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class SaleItemservice : ISaleItemservice
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public SaleItemservice(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SaleItemDto>?> GetAllSaleItemsBySaleIdAsync(int saleId)
        {
            try
            {
                var saleItems = await _repository.SaleItem.FindAllBySaleIdAsync(saleId);

                if (saleItems is null)
                {
                    return null;
                }

                var saleDetailDtos = _mapper.Map<IEnumerable<SaleItemDto>>(saleItems);

                return saleDetailDtos;
            }
            catch (EntityNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error retrieving Sale Items for Sale with id: {saleId}", ex);
            }
        }

        public async Task<SaleItemDto> GetSaleDetailBySaleAndDetailIdAsync(int saleId, int detailId)
        {
            try
            {
                var saleDetail = await _repository.SaleItem.FindBySaleAndDetailIdAsync(saleId, detailId);

                var saleDetailDto = _mapper.Map<SaleItemDto>(saleDetail);

                return saleDetailDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<SaleItemDto> GetSaleDetailByIdAsync(int saleDetailId)
        {
            try
            {
                var saleDetailEntity = await _repository.SaleItem.FindByIdAsync(saleDetailId);

                var saleDetailDto = _mapper.Map<SaleItemDto>(saleDetailEntity);

                return saleDetailDto;
            }
            catch (EntityNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error retrieving Sale Detail with Sale Detail with id: {saleDetailId}.", ex);
            }
        }

        public async Task<SaleItemDto> CreateSaleDetailAsync(SaleItemForCreateDto saleDetailToCreate)
        {
            try
            {
                var saleDetailEntity = _mapper.Map<SaleItem>(saleDetailToCreate);

                var createdEntity = _repository.SaleItem.Create(saleDetailEntity);
                await _repository.SaveChangesAsync();

                var saleDetailDto = _mapper.Map<SaleItemDto>(createdEntity);

                return saleDetailDto;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error creating new Sale Detail.", ex);
            }
        }

        public async Task UpdateSaleDetailAsync(SaleItemForUpdateDto saleDetailToUpdate)
        {
            try
            {
                if (saleDetailToUpdate is null)
                {
                    throw new ArgumentNullException(nameof(saleDetailToUpdate));
                }

                var saleDetailEntity = _mapper.Map<SaleItem>(saleDetailToUpdate);

                _repository.SaleItem.Update(saleDetailEntity);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error updating Sale Detail with id: {saleDetailToUpdate.Id}", ex);
            }
        }

        public async Task DeleteSaleDetailAsync(int id)
        {
            try
            {
                _repository.SaleItem.Delete(id);
                await _repository.SaveChangesAsync();
            }
            catch (EntityNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error deleting Sale Detail with id: {id}", ex);
            }
        }
    }
}
