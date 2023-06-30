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

        public async Task<RefundDetailDto> CreateRefundDetailAsync(RefundDetailForCreateDto RefundDetailToCreate)
        {
            ArgumentNullException.ThrowIfNull(RefundDetailToCreate);

            var RefundDetailEntity = _mapper.Map<RefundDetail>(RefundDetailToCreate);

            _repository.RefundDetail.Create(RefundDetailEntity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<RefundDetailDto>(RefundDetailEntity);
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
            var RefundDetails = await _repository.RefundDetail.FindAllAsync();

            return RefundDetails is null ?
                Enumerable.Empty<RefundDetailDto>() :
                _mapper.Map<IEnumerable<RefundDetailDto>>(RefundDetails);
        }

        public async Task<RefundDetailDto> GetRefundDetailByIdAsync(int id)
        {
            var RefundDetail = await _repository.RefundDetail.FindByIdAsync(id);

            return _mapper.Map<RefundDetailDto>(RefundDetail);
        }

        public async Task UpdateRefundDetailAsync(RefundDetailForUpdateDto RefundDetailToUpdate)
        {
            ArgumentNullException.ThrowIfNull(RefundDetailToUpdate);

            var RefundDetailEntity = _mapper.Map<RefundDetail>(RefundDetailToUpdate);

            _repository.RefundDetail.Update(RefundDetailEntity);
            await _repository.SaveChangesAsync();
        }
    }
}
