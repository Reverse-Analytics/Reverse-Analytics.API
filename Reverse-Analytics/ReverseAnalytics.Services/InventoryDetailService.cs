using AutoMapper;
using ReverseAnalytics.Domain.DTOs.InventoryDetail;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseAnalytics.Services
{
    public class InventoryDetailService : IInventoryDetailService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public InventoryDetailService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryDetailDto>> GetAllInventoryDetailsAsync()
        {
            try
            {
                var inventoryDetails = await _repository.InventoryDetail.FindAllAsync();
                var inventoryDetailDtos = _mapper.Map<IEnumerable<InventoryDetailDto>>(inventoryDetails);

                return inventoryDetailDtos;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<InventoryDetailDto>> GetAllByInventoryIdAsync(int inventoryId)
        {
            try
            {
                var inventoryDetails = await _repository.InventoryDetail.FindAllByInventoryIdAsync(inventoryId);

                var inventoryDetailDtos = _mapper.Map<IEnumerable<InventoryDetailDto>>(inventoryDetails);

                return inventoryDetailDtos;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error retrieving Inventory Details for Inventory with id: {inventoryId}", ex);
            }
        }

        public async Task<InventoryDetailDto> GetByInventoryAndDetailIdAsync(int inventoryId, int detailId)
        {
            try
            {
                var inventoryDetail = await _repository.InventoryDetail.FindByInventoryAndDetailIdAsync(inventoryId, detailId);

                var inventoryDetailDto = _mapper.Map<InventoryDetailDto>(inventoryDetail);

                return inventoryDetailDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<InventoryDetailDto> GetInventoryDetailByIdAsync(int inventoryDetailId)
        {
            try
            {
                var nventoryDetailEntity = await _repository.InventoryDetail.FindByIdAsync(inventoryDetailId);

                var inventoryDetailDto = _mapper.Map<InventoryDetailDto>(nventoryDetailEntity);

                return inventoryDetailDto;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error retrieving Inventory Detail with Inventory Detail with id: {inventoryDetailId}.", ex);
            }
        }

        public async Task<InventoryDetailDto> CreateInventoryDetailAsync(InventoryDetailForCreateDto InventoryDetailToCreate)
        {
            try
            {
                var inventoryDetailEntity = _mapper.Map<InventoryDetail>(InventoryDetailToCreate);

                var createdEntity = _repository.InventoryDetail.Create(inventoryDetailEntity);
                await _repository.SaveChangesAsync();

                var inventoryDetailDto = _mapper.Map<InventoryDetailDto>(createdEntity);

                return inventoryDetailDto;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error creating new Inventory Detail.", ex);
            }
        }

        public async Task UpdateInventoryDetailAsync(InventoryDetailForUpdateDto inventoryDetailToUpdate)
        {
            try
            {
                if (inventoryDetailToUpdate is null)
                {
                    throw new ArgumentNullException(nameof(inventoryDetailToUpdate));
                }

                var inventoryDetailEntity = _mapper.Map<InventoryDetail>(inventoryDetailToUpdate);

                _repository.InventoryDetail.Update(inventoryDetailEntity);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error updating Inventory Detail with id: {inventoryDetailToUpdate.Id}", ex);
            }
        }

        public async Task DeleteInventoryDetailAsync(int id)
        {
            try
            {
                _repository.InventoryDetail.Delete(id);
                await _repository.SaveChangesAsync();
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error deleting Inventory Detail with id: {id}", ex);
            }
        }
    }
}
