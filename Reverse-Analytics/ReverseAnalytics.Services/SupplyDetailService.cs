using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SupplyItem;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class SupplyItemservice : ISupplyItemservice
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public SupplyItemservice(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplyItemDto>> GetAllSupplyItemsAsync()
        {
            try
            {
                var supplyItems = await _repository.SupplyItem.FindAllAsync();

                var supplyDetailDtos = _mapper.Map<IEnumerable<SupplyItemDto>>(supplyItems);

                return supplyDetailDtos;
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SupplyItemDto>> GetAllSupplyItemsByProductIdAsync(int productId)
        {
            try
            {
                var supplyItems = await _repository.SupplyItem.FindAllByProductIdAsync(productId);

                var supplyDetailDtos = _mapper.Map<IEnumerable<SupplyItemDto>>(supplyItems);

                return supplyDetailDtos;
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SupplyItemDto> GetBySupplyAndDetailIdAsync(int supplyId, int detailId)
        {
            try
            {
                var supplyDetail = await _repository.SupplyItem.FindBySupplyAndDetailIdAsync(supplyId, detailId);

                var supplyDetailDto = _mapper.Map<SupplyItemDto>(supplyDetail);

                return supplyDetailDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<SupplyItemDto>> GetAllSupplyItemsBySupplyIdAsync(int supplyId)
        {
            try
            {
                var supplyItems = await _repository.SupplyItem.FindAllBySupplyIdAsync(supplyId);

                var supplyDetailDtos = _mapper.Map<IEnumerable<SupplyItemDto>>(supplyItems);

                return supplyDetailDtos;
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SupplyItemDto> GetSupplyDetailByIdAsync(int supplyDetailid)
        {
            try
            {
                var supplyDetail = await _repository.SupplyItem.FindByIdAsync(supplyDetailid);

                var supplyDetailDto = _mapper.Map<SupplyItemDto>(supplyDetail);

                return supplyDetailDto;
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SupplyItemDto> CreateSupplyDetailAsync(SupplyItemForCreateDto supplyDetailToCreate)
        {
            try
            {
                var supplyDetailEntity = _mapper.Map<SupplyItem>(supplyDetailToCreate);

                var createdEntity = _repository.SupplyItem.Create(supplyDetailEntity);
                await _repository.SaveChangesAsync();

                var supplyDetailDto = _mapper.Map<SupplyItemDto>(createdEntity);

                return supplyDetailDto;
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateSupplyDetailAsync(SupplyItemForUpdateDto supplyDetailToUpdate)
        {
            try
            {
                var supplyDetailEntity = _mapper.Map<SupplyItem>(supplyDetailToUpdate);

                _repository.SupplyItem.Update(supplyDetailEntity);
                await _repository.SaveChangesAsync();
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteSupplyDetailAsync(int supplyDetailid)
        {
            try
            {
                _repository.SupplyItem.Delete(supplyDetailid);
                await _repository.SaveChangesAsync();
            }
            catch (NotFoundException)
            {

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
