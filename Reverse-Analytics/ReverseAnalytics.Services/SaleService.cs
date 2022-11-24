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

                if (saleDetails != null && saleDetails.Count > 0)
                {
                    _repository.SaleDetail.CreateRange(saleDetails);
                }

                await _repository.SaveChangesAsync();

                var SaleDto = _mapper.Map<SaleDto>(saleEntity);

                return SaleDto;
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

        //#region Sale Item

        //public async Task<IEnumerable<SaleItemDto>?> GetAllSaleItemsAsync(int SaleId, int pageSize, int pageNumber)
        //{
        //    try
        //    {
        //        var SaleItems = await _repository.SaleItem.FindAllBySaleIdAsync(SaleId, pageSize, pageNumber);

        //        if (SaleItems is null)
        //        {
        //            return null;
        //        }

        //        var SaleItemDtos = _mapper.Map<IEnumerable<SaleItemDto>>(SaleItems);

        //        return SaleItemDtos;
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"There was an error retrieving Sale Items for Sale with id: {SaleId}", ex);
        //    }
        //}

        //public async Task<SaleItemDto> GetSaleItemByIdAsync(int SaleId, int SaleItemId)
        //{
        //    try
        //    {
        //        var SaleItemEntity = await _repository.SaleItem.FindByIdAsync(SaleItemId);

        //        var SaleItemDto = _mapper.Map<SaleItemDto>(SaleItemEntity);

        //        return SaleItemDto;
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"There was an error retrieving Sale Item with Sale id: {SaleId} and Sale Item id: {SaleItemId}.", ex);
        //    }
        //}

        //public async Task<SaleItemDto> CreateSaleItemAsync(SaleItemForCreate SaleItemToCreate)
        //{
        //    try
        //    {
        //        var SaleItemEntity = _mapper.Map<SaleDetail>(SaleItemToCreate);

        //        var createdEntity = _repository.SaleItem.Create(SaleItemEntity);
        //        await _repository.SaveChangesAsync();

        //        var SaleItemDto = _mapper.Map<SaleItemDto>(createdEntity);

        //        return SaleItemDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("There was an error creating new Sale Item.", ex);
        //    }
        //}

        //public async Task UpdateSaleItemAsync(SaleItemForUpdate SaleItemToUpdate)
        //{
        //    try
        //    {
        //        if (SaleItemToUpdate is null)
        //        {
        //            throw new ArgumentNullException(nameof(SaleItemToUpdate));
        //        }

        //        var SaleItemDto = _mapper.Map<SaleDetail>(SaleItemToUpdate);

        //        _repository.SaleItem.Update(SaleItemDto);
        //        await _repository.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"There was an error updating Sale Item with id: {SaleItemToUpdate.Id}", ex);
        //    }
        //}

        //public async Task DeleteSaleItemAsync(int id)
        //{
        //    try
        //    {
        //        _repository.SaleItem.Delete(id);
        //        await _repository.SaveChangesAsync();
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"There was an error deleting Sale Item with id: {id}", ex);
        //    }
        //}

        //#endregion
    }
}