using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IOrderItemRepository : IRepositoryBase<OrderDetail>
    {
        public Task<IEnumerable<OrderDetail>> FindAllByOrderIdAsync(int orderId, int pageSize, int pageNumber);
    }
}
