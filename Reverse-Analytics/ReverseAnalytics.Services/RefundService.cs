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

            var currentRefund = await _repository.Refund.FindByIdAsync(refundToUpdate.Id);

            if (currentRefund == null)
            {
                throw new NotFoundException($"Refund with id: {refundToUpdate.Id} does not exist.");
            }

            UpdateRefundItems(refundEntity, currentRefund);


            _repository.Refund.Update(refundEntity);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteRefundAsync(int id)
        {
            var refund = await _repository.Refund.FindByIdAsync(id);

            if (refund == null)
            {
                throw new NotFoundException($"Refund with id: {id} does not exist.");
            }

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

        private void UpdateRefundItems(Refund refundToUpdate, Refund refundEntity)
        {
            if (refundToUpdate.RefundItems is null || refundEntity.RefundItems is null)
            {
                return;
            }

            var newItems = refundToUpdate.RefundItems.ToList() ?? new List<RefundItem>();
            var currentItems = refundEntity.RefundItems.ToList() ?? new List<RefundItem>();

            var itemsToAdd = newItems.Where(newItem => !currentItems.Any(currentItem => currentItem.Id == newItem.Id));
            var itemsToDelete = currentItems.Where(currentItem => !newItems.Any(newItem => newItem.Id == currentItem.Id));

            if (itemsToAdd.Any())
            {
                _repository.RefundItem.CreateRange(itemsToAdd);
            }

            if (itemsToDelete.Any())
            {
                _repository.RefundItem.DeleteRange(itemsToDelete);
            }
        }

        private static void ValidateNotNull<T>(T value)
        {
            ArgumentNullException.ThrowIfNull(value);
        }
    }
}
