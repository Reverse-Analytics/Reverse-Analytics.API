using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Refund;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class RefundService : IRefundService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public RefundService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RefundDto> CreateRefundAsync(RefundForCreateDto refundToCreate)
        {
            ValidateNotNull(refundToCreate);

            var sale = await _repository.Sale.FindByIdAsync(refundToCreate.SaleId) ??
                throw new NotFoundException($"Sale with id: {refundToCreate.SaleId} does not exist.");

            if (refundToCreate.TotalAmount > sale.TotalDue)
            {
                throw new RefundExceedsSaleException($"Refund amount: {refundToCreate.TotalAmount} cannot be greater than Sale total due: {sale.TotalDue}");
            }

            var refundEntity = _mapper.Map<Refund>(refundToCreate);

            var saleDebt = await _repository.SaleDebt.FindBySaleIdAsync(sale.Id);

            _repository.Refund.Create(refundEntity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<RefundDto>(refundEntity);
        }

        public async Task UpdateRefundAsync(RefundForUpdateDto refundToUpdate)
        {
            ValidateNotNull(refundToUpdate);

            var sale = await _repository.Sale.FindByIdAsync(refundToUpdate.SaleId) ??
                throw new NotFoundException($"Sale with id: {refundToUpdate.SaleId} does not exist.");

            if (refundToUpdate.TotalAmount > sale.TotalDue)
            {
                throw new RefundExceedsSaleException($"Refund amount: {refundToUpdate.TotalAmount} cannot be greater than Sale total due: {sale.TotalDue}");
            }

            var refundEntity = _mapper.Map<Refund>(refundToUpdate);

            var saleDebt = await _repository.SaleDebt.FindBySaleIdAsync(sale.Id);
            UpdateDebt(sale, saleDebt, refundEntity);

            _repository.Refund.Update(refundEntity);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteRefundAsync(int id)
        {
            _repository.Refund.Delete(id);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<RefundDto>> GetAllRefundsAsync()
        {
            var refunds = await _repository.Refund.FindRefundsAsync();

            return refunds is null ?
                Enumerable.Empty<RefundDto>() :
                _mapper.Map<IEnumerable<RefundDto>>(refunds);
        }

        public async Task<RefundDto> GetRefundByIdAsync(int id)
        {
            var refund = await _repository.Refund.FindByIdAsync(id);

            return _mapper.Map<RefundDto>(refund);
        }

        private static void UpdateDebt(Sale sale, SaleDebt debt, Refund refund)
        {
            ValidateNotNull(refund);
            ValidateNotNull(debt);

            var actualDebtAmount = sale.TotalDue - sale.TotalPaid;
            var leftOver = debt.TotalDue - debt.TotalPaid;

            if (actualDebtAmount <= 0)
            {
                return;
            }


        }

        private static void ValidateNotNull<T>(T value)
        {
            ArgumentNullException.ThrowIfNull(value);
        }
    }
}
