using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Refund;
using ReverseAnalytics.Domain.Entities;
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
            ArgumentNullException.ThrowIfNull(refundToCreate);

            var refundEntity = _mapper.Map<Refund>(refundToCreate);

            _repository.Refund.Create(refundEntity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<RefundDto>(refundEntity);
        }

        public async Task DeleteRefundAsync(int id)
        {
            _repository.Refund.Delete(id);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<RefundDto>> GetAllRefundsAsync()
        {
            var refunds = await _repository.Refund.FindAllAsync();

            return refunds is null ?
                Enumerable.Empty<RefundDto>() :
                _mapper.Map<IEnumerable<RefundDto>>(refunds);
        }

        public async Task<RefundDto> GetRefundByIdAsync(int id)
        {
            var refund = await _repository.Refund.FindByIdAsync(id);

            return _mapper.Map<RefundDto>(refund);
        }

        public async Task UpdateRefundAsync(RefundForUpdateDto refundToUpdate)
        {
            ArgumentNullException.ThrowIfNull(refundToUpdate);

            var refundEntity = _mapper.Map<Refund>(refundToUpdate);

            _repository.Refund.Update(refundEntity);
            await _repository.SaveChangesAsync();
        }
    }
}
