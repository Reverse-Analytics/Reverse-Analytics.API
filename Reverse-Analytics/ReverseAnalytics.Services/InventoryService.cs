using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Inventory;
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
    public class InventoryService : IInventoryService
    {
        private readonly IMapper _mapper;
        private readonly ICommonRepository _repository;

        public InventoryService(IMapper mapper, ICommonRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<InventoryDto>> GetAllInventoriesAsync()
        {
            try
            {
                var Inventorys = await _repository.Inventory.FindAllAsync();

                var InventoryDtos = _mapper.Map<IEnumerable<InventoryDto>>(Inventorys);

                return InventoryDtos;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving Inventorys.", ex);
            }
        }

        public async Task<InventoryDto> GetInventoryByIdAsync(int id)
        {
            try
            {
                var Inventory = await _repository.Inventory.FindByIdAsync(id);

                var InventoryDto = _mapper.Map<InventoryDto>(Inventory);

                return InventoryDto;
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
                throw new Exception($"Error retrieving Inventory with id: {id}", ex);
            }
        }

        public async Task<InventoryDto> CreateInventoryAsync(InventoryForCreateDto InventoryToCreate)
        {
            try
            {
                var InventoryEntity = _mapper.Map<Inventory>(InventoryToCreate);

                InventoryEntity = _repository.Inventory.Create(InventoryEntity);

                await _repository.SaveChangesAsync();

                var InventoryDto = _mapper.Map<InventoryDto>(InventoryEntity);

                return InventoryDto;
            }
            catch (AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error creating new Inventory.", ex);
            }
        }

        public async Task UpdateInventoryAsync(InventoryForUpdateDto InventoryToUpdate)
        {
            try
            {
                var InventoryEntity = _mapper.Map<Inventory>(InventoryToUpdate);

                _repository.Inventory.Update(InventoryEntity);
                await _repository.Inventory.SaveChangesAsync();
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
                throw new Exception($"There was an error updating Inventory with id: {InventoryToUpdate?.Id}.", ex);
            }
        }

        public async Task DeleteInventoryAsync(int id)
        {
            try
            {
                _repository.Inventory.Delete(id);
                await _repository.Inventory.SaveChangesAsync();
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error deleting Inventory with id: {id}.", ex);
            }
        }
    }
}
