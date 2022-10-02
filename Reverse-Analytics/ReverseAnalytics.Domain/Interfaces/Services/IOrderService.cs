using ReverseAnalytics.Domain.DTOs.Order;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderDto>> GetAllOrdersAsync(int pageSize, int pageNumber);
        public Task<OrderDto> GetOrderByIdAsync(int id);
        public Task<OrderDto> CreateOrderAsync(OrderForCreate orderToCreate);
        public Task UpdateOrderAsync(OrderForUpdate orderToUpdate);
        public Task DeleteOrderAsync(int id);
    }
}
