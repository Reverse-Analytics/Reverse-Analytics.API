using ReverseAnalytics.Doman.DTOs.RefundItem;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IRefundItemservice
    {
        public Task<IEnumerable<RefundItemDto>> GetAllRefundItemsAsync();
        public Task<IEnumerable<RefundItemDto>> GetRefundItemsByRefundIdAsync(int refundId);
        public Task<RefundItemDto> GetRefundDetailByIdAsync(int id);
        public Task<RefundItemDto> CreateRefundDetailAsync(RefundItemForCreateDto refundDetailToCreate);
        public Task UpdateRefundDetailAsync(RefundItemForUpdateDto refundDetailToUpdate);
        public Task DeleteRefundDetailAsync(int id);
    }
}
