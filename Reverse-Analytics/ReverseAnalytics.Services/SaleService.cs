﻿using AutoMapper;
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
                var sales = await _repository.Sale.FindAllAsync();

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
            catch (EntityNotFoundException ex)
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

        public async Task<IEnumerable<SaleDto>> GetSalesByCustomerIdAsync(int customerId)
        {
            var saleEntities = await _repository.Sale.FindAllByCustomerIdAsync(customerId);

            var saleDtos = _mapper.Map<IEnumerable<SaleDto>>(saleEntities);

            return saleDtos ?? Enumerable.Empty<SaleDto>();
        }

        public async Task<SaleDto> CreateSaleAsync(SaleForCreateDto saleToCreate)
        {
            var saleEntity = _mapper.Map<Sale>(saleToCreate);

            saleEntity = _repository.Sale.Create(saleEntity);

            await _repository.SaveChangesAsync();
            saleEntity.Customer = await _repository.Customer.FindByIdAsync(saleEntity.CustomerId);

            var saleDto = _mapper.Map<SaleDto>(saleEntity);

            return saleDto;
        }

        public async Task UpdateSaleAsync(SaleForUpdateDto saleToUpdate)
        {
            try
            {
                var saleEntity = _mapper.Map<Sale>(saleToUpdate);

                await _repository.SaleItem.DeleteRangeBySaleIdAsync(saleToUpdate.Id);
                _repository.SaleItem.CreateRange(saleEntity.SaleItems);

                _repository.Sale.Update(saleEntity);
                await _repository.SaveChangesAsync();
            }
            catch (EntityNotFoundException ex)
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
            _repository.Sale.Delete(id);
            await _repository.SaveChangesAsync();
        }
    }
}