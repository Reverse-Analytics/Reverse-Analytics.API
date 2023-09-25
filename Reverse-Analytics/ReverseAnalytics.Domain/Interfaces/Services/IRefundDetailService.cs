using ReverseAnalytics.Doman.DTOs.RefundItem;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IRefundDetailService
    {
        public Task<IEnumerable<RefundItemDto>> GetAllRefundDetailsAsync();
        public Task<IEnumerable<RefundItemDto>> GetRefundDetailsByRefundIdAsync(int refundId);
        public Task<RefundItemDto> GetRefundDetailByIdAsync(int id);
        public Task<RefundItemDto> CreateRefundDetailAsync(RefundItemForCreateDto refundDetailToCreate);
        public Task UpdateRefundDetailAsync(RefundItemForUpdateDto refundDetailToUpdate);
        public Task DeleteRefundDetailAsync(int id);
    }
}
