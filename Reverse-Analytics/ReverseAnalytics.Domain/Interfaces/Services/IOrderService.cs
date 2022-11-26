using ReverseAnalytics.Domain.DTOs.Order;
using ReverseAnalytics.Domain.DTOs.OrderItem;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderDto>> GetAllOrdersAsync(int pageSize, int pageNumber);
        public Task<OrderDto> GetOrderByIdAsync(int id);
        public Task<OrderDto> CreateOrderAsync(OrderForCreate orderToCreate);
        public Task UpdateOrderAsync(OrderForUpdate orderToUpdate);
        public Task DeleteOrderAsync(int id);

        public Task<IEnumerable<OrderItemDto>?> GetAllOrderItemsAsync(int orderId, int pageSize, int pageNumber);
        public Task<OrderItemDto> GetOrderItemByIdAsync(int orderId, int orderItemId);
        public Task<OrderItemDto> CreateOrderItemAsync(OrderItemForCreate orderItemToCreate);
        public Task UpdateOrderItemAsync(OrderItemForUpdate orderItemToUpdate);
        public Task DeleteOrderItemAsync(int orderItemId);
    }
}
