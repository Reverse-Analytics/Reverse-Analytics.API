using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Supply;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class SupplyService : ISupplyService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public SupplyService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplyDto>> GetAllSuppliesAsync()
        {
            try
            {
                var supplies = await _repository.Supply.FindAllAsync();

                var supplyDtos = _mapper.Map<IEnumerable<SupplyDto>>(supplies);

                return supplyDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SupplyDto>> GetAllSuppliesBySupplierIdAsync(int supplierId)
        {
            try
            {
                var supplies = await _repository.Supply.FindAllBySupplierIdAsync(supplierId);

                var supplyDtos = _mapper.Map<IEnumerable<SupplyDto>>(supplies);

                return supplyDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SupplyDto> GetSupplyByIdAsync(int supplyId)
        {
            try
            {
                var supply = await _repository.Supply.FindByIdAsync(supplyId);

                var supplyDto = _mapper.Map<SupplyDto>(supply);

                return supplyDto;
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

        public async Task<SupplyDto> CreateSupplyAsync(SupplyForCreateDto supplyToCreate)
        {
            try
            {
                var supplyEntity = _mapper.Map<Supply>(supplyToCreate);

                _repository.Supply.Create(supplyEntity);
                await _repository.SaveChangesAsync();

                var supplyDto = _mapper.Map<SupplyDto>(supplyEntity);

                return supplyDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateSupplyAsync(SupplyForUpdateDto supplyToUpdate)
        {
            try
            {
                var supplyEntity = _mapper.Map<Supply>(supplyToUpdate);

                _repository.Supply.Update(supplyEntity);
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

        public async Task DeleteSupplyAsync(int supplyId)
        {
            try
            {
                _repository.Supply.Delete(supplyId);
                await _repository.SaveChangesAsync();
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception)
            {

            }
        }
    }
}
