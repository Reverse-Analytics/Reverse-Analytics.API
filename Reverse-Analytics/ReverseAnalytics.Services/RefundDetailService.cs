using AutoMapper;
using ReverseAnalytics.Doman.DTOs.RefundItem;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class RefundDetailService : IRefundDetailService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public RefundDetailService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RefundItemDto> CreateRefundDetailAsync(RefundItemForCreateDto refundDetailToCreate)
        {
            ArgumentNullException.ThrowIfNull(refundDetailToCreate);

            var refundDetailEntity = _mapper.Map<RefundItem>(refundDetailToCreate);

            _repository.RefundItem.Create(refundDetailEntity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<RefundItemDto>(refundDetailEntity);
        }

        public async Task<IEnumerable<RefundItemDto>> GetRefundDetailsByRefundIdAsync(int refundId)
        {
            var refundDetails = await _repository.RefundItem.FindAllByRefundIdAsync(refundId);

            return _mapper.Map<IEnumerable<RefundItemDto>>(refundDetails);
        }

        public async Task DeleteRefundDetailAsync(int id)
        {
            _repository.RefundItem.Delete(id);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<RefundItemDto>> GetAllRefundDetailsAsync()
        {
            var refundDetails = await _repository.RefundItem.FindAllAsync();

            return refundDetails is null ?
                Enumerable.Empty<RefundItemDto>() :
                _mapper.Map<IEnumerable<RefundItemDto>>(refundDetails);
        }

        public async Task<RefundItemDto> GetRefundDetailByIdAsync(int id)
        {
            var refundDetail = await _repository.RefundItem.FindByIdAsync(id);

            return _mapper.Map<RefundItemDto>(refundDetail);
        }

        public async Task UpdateRefundDetailAsync(RefundItemForUpdateDto refundDetailToUpdate)
        {
            ArgumentNullException.ThrowIfNull(refundDetailToUpdate);

            var refundDetailEntity = _mapper.Map<RefundItem>(refundDetailToUpdate);

            _repository.RefundItem.Update(refundDetailEntity);
            await _repository.SaveChangesAsync();
        }
    }
}
