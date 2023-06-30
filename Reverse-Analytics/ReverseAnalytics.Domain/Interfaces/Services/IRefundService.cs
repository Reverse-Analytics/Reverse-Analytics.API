using ReverseAnalytics.Domain.DTOs.Refund;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IRefundService
    {
        public Task<IEnumerable<RefundDto>> GetAllRefundsAsync();
        public Task<RefundDto> GetRefundByIdAsync(int id);
        public Task<RefundDto> CreateRefundAsync(RefundForCreateDto refundToCreate);
        public Task UpdateRefundAsync(RefundForUpdateDto refundToUpdate);
        public Task DeleteRefundAsync(int id);
    }
}
