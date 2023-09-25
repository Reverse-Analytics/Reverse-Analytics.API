using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SaleItem;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class SaleDetailService : ISaleDetailService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public SaleDetailService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SaleItemDto>?> GetAllSaleDetailsBySaleIdAsync(int saleId)
        {
            try
            {
                var saleDetails = await _repository.SaleDetail.FindAllBySaleIdAsync(saleId);

                if (saleDetails is null)
                {
                    return null;
                }

                var saleDetailDtos = _mapper.Map<IEnumerable<SaleItemDto>>(saleDetails);

                return saleDetailDtos;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error retrieving Sale Details for Sale with id: {saleId}", ex);
            }
        }

        public async Task<SaleItemDto> GetSaleDetailBySaleAndDetailIdAsync(int saleId, int detailId)
        {
            try
            {
                var saleDetail = await _repository.SaleDetail.FindBySaleAndDetailIdAsync(saleId, detailId);

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
                var saleDetailEntity = await _repository.SaleDetail.FindByIdAsync(saleDetailId);

                var saleDetailDto = _mapper.Map<SaleItemDto>(saleDetailEntity);

                return saleDetailDto;
            }
            catch (NotFoundException ex)
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

                var createdEntity = _repository.SaleDetail.Create(saleDetailEntity);
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

                _repository.SaleDetail.Update(saleDetailEntity);
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
                _repository.SaleDetail.Delete(id);
                await _repository.SaveChangesAsync();
            }
            catch (NotFoundException ex)
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
