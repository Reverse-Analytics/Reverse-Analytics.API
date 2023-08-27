using AutoMapper;
using ReverseAnalytics.Domain.DTOs.SaleDebt;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class SaleDebtService : ISaleDebtService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public SaleDebtService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SaleDebtDto> CreateSaleDebtAsync(SaleDebtForCreateDto saleDebtToCreate)
        {
            ArgumentNullException.ThrowIfNull(saleDebtToCreate);

            var saleDebtEntity = _mapper.Map<SaleDebt>(saleDebtToCreate);

            _repository.SaleDebt.Create(saleDebtEntity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<SaleDebtDto>(saleDebtEntity);
        }

        public async Task DeleteSaleDebtAsync(int id)
        {
            _repository.SaleDebt.Delete(id);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<SaleDebtDto>> GetAllSaleDebtsAsync()
        {
            var saleDebts = await _repository.SaleDebt.FindAllAsync();

            return saleDebts is null ?
                Enumerable.Empty<SaleDebtDto>() :
                _mapper.Map<IEnumerable<SaleDebtDto>>(saleDebts);
        }

        public async Task<SaleDebtDto> SettleDebtAsync(int id)
        {
            var debt = await _repository.SaleDebt.FindByIdAsync(id);

            if (debt is null)
            {
                throw new NotFoundException();
            }

            debt.Sale = await _repository.Sale.FindByIdAsync(debt.SaleId);
            debt.Sale.Customer = await _repository.Customer.FindByIdAsync(debt.Sale.CustomerId);
            debt.TotalPaid = debt.TotalDue;
            debt.Status = Domain.Enums.DebtStatus.Closed;
            debt.ClosedDate = DateTime.Now;
            await _repository.SaveChangesAsync();

            return _mapper.Map<SaleDebtDto>(debt);
        }

        public async Task<SaleDebtDto> MakePaymentAsync(int id, decimal amount)
        {
            var debt = await _repository.SaleDebt.FindByIdAsync(id);

            if (debt is null)
            {
                throw new NotFoundException();
            }

            if (debt.TotalDue - (debt.TotalPaid + amount) <= 0)
            {
                return await SettleDebtAsync(id);
            }

            debt.Sale = await _repository.Sale.FindByIdAsync(debt.SaleId);
            debt.Sale.Customer = await _repository.Customer.FindByIdAsync(debt.Sale.CustomerId);
            debt.TotalPaid += amount;
            await _repository.SaveChangesAsync();

            return _mapper.Map<SaleDebtDto>(debt);
        }

        public async Task<SaleDebtDto> GetSaleDebtByIdAsync(int id)
        {
            var saleDebt = await _repository.SaleDebt.FindByIdAsync(id);

            return _mapper.Map<SaleDebtDto>(saleDebt);
        }

        public async Task<IEnumerable<SaleDebtDto>> GetSaleDebtsBySaleIdAsync(int saleId)
        {
            var saleDebts = await _repository.SaleDebt.FindAllBySaleIdAsync(saleId);

            return saleDebts is null ?
                Enumerable.Empty<SaleDebtDto>() :
                _mapper.Map<IEnumerable<SaleDebtDto>>(saleDebts);
        }

        public async Task UpdateSaleDebtAsync(SaleDebtForUpdateDto saleDebtToUpdate)
        {
            ArgumentNullException.ThrowIfNull(saleDebtToUpdate);

            var saleDebtEntity = _mapper.Map<SaleDebt>(saleDebtToUpdate);

            _repository.SaleDebt.Update(saleDebtEntity);
            await _repository.SaveChangesAsync();
        }
    }
}
