using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SupplyDetail;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class SupplyDetailService : ISupplyDetailService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public SupplyDetailService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplyDetailDto>> GetAllSupplyDetailsAsync()
        {
            try
            {
                var supplyDetails = await _repository.SupplyDetail.FindAllAsync();

                var supplyDetailDtos = _mapper.Map<IEnumerable<SupplyDetailDto>>(supplyDetails);

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

        public async Task<IEnumerable<SupplyDetailDto>> GetAllSupplyDetailsByProductIdAsync(int productId)
        {
            try
            {
                var supplyDetails = await _repository.SupplyDetail.FindAllByProductIdAsync(productId);

                var supplyDetailDtos = _mapper.Map<IEnumerable<SupplyDetailDto>>(supplyDetails);

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

        public async Task<SupplyDetailDto> GetBySupplyAndDetailIdAsync(int supplyId, int detailId)
        {
            try
            {
                var supplyDetail = await _repository.SupplyDetail.FindBySupplyAndDetailIdAsync(supplyId, detailId);

                var supplyDetailDto = _mapper.Map<SupplyDetailDto>(supplyDetail);

                return supplyDetailDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<SupplyDetailDto>> GetAllSupplyDetailsBySupplyIdAsync(int supplyId)
        {
            try
            {
                var supplyDetails = await _repository.SupplyDetail.FindAllBySupplyIdAsync(supplyId);

                var supplyDetailDtos = _mapper.Map<IEnumerable<SupplyDetailDto>>(supplyDetails);

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

        public async Task<SupplyDetailDto> GetSupplyDetailByIdAsync(int supplyDetailid)
        {
            try
            {
                var supplyDetail = await _repository.SupplyDetail.FindByIdAsync(supplyDetailid);

                var supplyDetailDto = _mapper.Map<SupplyDetailDto>(supplyDetail);

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

        public async Task<SupplyDetailDto> CreateSupplyDetailAsync(SupplyDetailForCreateDto supplyDetailToCreate)
        {
            try
            {
                var supplyDetailEntity = _mapper.Map<SupplyDetail>(supplyDetailToCreate);

                var createdEntity = _repository.SupplyDetail.Create(supplyDetailEntity);
                await _repository.SaveChangesAsync();

                var supplyDetailDto = _mapper.Map<SupplyDetailDto>(createdEntity);

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

        public async Task UpdateSupplyDetailAsync(SupplyDetailForUpdateDto supplyDetailToUpdate)
        {
            try
            {
                var supplyDetailEntity = _mapper.Map<SupplyDetail>(supplyDetailToUpdate);

                _repository.SupplyDetail.Update(supplyDetailEntity);
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
                _repository.SupplyDetail.Delete(supplyDetailid);
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
