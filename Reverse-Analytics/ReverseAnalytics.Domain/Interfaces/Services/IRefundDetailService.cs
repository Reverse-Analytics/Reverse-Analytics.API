using ReverseAnalytics.Domain.DTOs.RefundDetail;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IRefundDetailService
    {
        public Task<IEnumerable<RefundDetailDto>> GetAllRefundDetailsAsync();
        public Task<IEnumerable<RefundDetailDto>> GetRefundDetailsByRefundIdAsync(int refundId);
        public Task<RefundDetailDto> GetRefundDetailByIdAsync(int id);
        public Task<RefundDetailDto> CreateRefundDetailAsync(RefundDetailForCreateDto refundDetailToCreate);
        public Task UpdateRefundDetailAsync(RefundDetailForUpdateDto refundDetailToUpdate);
        public Task DeleteRefundDetailAsync(int id);
    }
}
