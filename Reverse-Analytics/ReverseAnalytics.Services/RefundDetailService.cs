using AutoMapper;
using ReverseAnalytics.Domain.DTOs.RefundDetail;
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

        public async Task<RefundDetailDto> CreateRefundDetailAsync(RefundDetailForCreateDto refundDetailToCreate)
        {
            ArgumentNullException.ThrowIfNull(refundDetailToCreate);

            var refundDetailEntity = _mapper.Map<RefundDetail>(refundDetailToCreate);

            _repository.RefundDetail.Create(refundDetailEntity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<RefundDetailDto>(refundDetailEntity);
        }

        public async Task<IEnumerable<RefundDetailDto>> GetRefundDetailsByRefundIdAsync(int refundId)
        {
            var refundDetails = await _repository.RefundDetail.FindAllByRefundIdAsync(refundId);

            return _mapper.Map<IEnumerable<RefundDetailDto>>(refundDetails);
        }

        public async Task DeleteRefundDetailAsync(int id)
        {
            _repository.RefundDetail.Delete(id);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<RefundDetailDto>> GetAllRefundDetailsAsync()
        {
            var refundDetails = await _repository.RefundDetail.FindAllAsync();

            return refundDetails is null ?
                Enumerable.Empty<RefundDetailDto>() :
                _mapper.Map<IEnumerable<RefundDetailDto>>(refundDetails);
        }

        public async Task<RefundDetailDto> GetRefundDetailByIdAsync(int id)
        {
            var refundDetail = await _repository.RefundDetail.FindByIdAsync(id);

            return _mapper.Map<RefundDetailDto>(refundDetail);
        }

        public async Task UpdateRefundDetailAsync(RefundDetailForUpdateDto refundDetailToUpdate)
        {
            ArgumentNullException.ThrowIfNull(refundDetailToUpdate);

            var refundDetailEntity = _mapper.Map<RefundDetail>(refundDetailToUpdate);

            _repository.RefundDetail.Update(refundDetailEntity);
            await _repository.SaveChangesAsync();
        }
    }
}
