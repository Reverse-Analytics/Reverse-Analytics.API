using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Debt;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseAnalytics.Services
{
    public class DebtService : IDebtService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public DebtService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DebtDto>> GetAllDebtsAsync()
        {
            try
            {
                var debts = await _repository.Debt.FindAllAsync();

                var debtDtos = _mapper.Map<IEnumerable<DebtDto>>(debts);

                return debtDtos;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<DebtDto>> GetAllDebtsByPersonIdAsync(int personId)
        {
            try
            {
                var debts = await _repository.Debt.FindAllByPersonIdAsync(personId);

                var debtDtos = _mapper.Map<IEnumerable<DebtDto>>(debts);

                return debtDtos;
            }
            catch
            {
                throw;
            }
        }

        public async Task<DebtDto> GetDebtByIdAsync(int id)
        {
            try
            {
                var debt = await _repository.Debt.FindByIdAsync(id);

                var debtDto = _mapper.Map<DebtDto>(debt);

                return debtDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<DebtDto> CreateDebtAsync(DebtForCreateDto debtToCreate)
        {
            try
            {
                var debtEntity = _mapper.Map<Debt>(debtToCreate);

                var createdEntity = _repository.Debt.Create(debtEntity);
                await _repository.SaveChangesAsync();

                var debtDto = _mapper.Map<DebtDto>(createdEntity);

                return debtDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateDebtAsync(DebtForUpdateDto debtToUpdate)
        {
            try
            {
                var debtEntity = _mapper.Map<Debt>(debtToUpdate);

                _repository.Debt.Update(debtEntity);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteDebtAsync(int id)
        {
            try
            {
                _repository.Debt.Delete(id);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
