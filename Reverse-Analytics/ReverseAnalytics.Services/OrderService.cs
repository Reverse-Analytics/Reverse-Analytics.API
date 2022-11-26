using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Order;
using ReverseAnalytics.Domain.DTOs.OrderItem;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public OrderService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(int pageSize, int pageNumber)
        {
            try
            {
                var orders = await _repository.Order.FindAllAsync(pageSize, pageNumber);

                var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

                return orderDtos;
            }
            catch(AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception("Error while retrieving orders.", ex);
            }
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            try
            {
                var order = await _repository.Order.FindByIdAsync(id);

                var orderDto = _mapper.Map<OrderDto>(order);

                return orderDto;
            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch(AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception($"Error retrieving Order with id: {id}", ex);
            }
        }

        public async Task<OrderDto> CreateOrderAsync(OrderForCreate orderToCreate)
        {
            try
            {
                var orderEntity = _mapper.Map<Order>(orderToCreate);
                var orderItemEntities = _mapper.Map<ICollection<OrderDetail>>(orderToCreate?.OrderItems);

                orderEntity = _repository.Order.Create(orderEntity);

                if (orderItemEntities != null && orderItemEntities.Count > 0)
                {
                    _repository.OrderItem.CreateRange(orderItemEntities);
                }

                await _repository.SaveChangesAsync();

                var orderDto = _mapper.Map<OrderDto>(orderEntity);

                return orderDto;
            }
            catch(AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception("There was an error creating new Order.", ex);
            }
        }

        public async Task UpdateOrderAsync(OrderForUpdate orderToUpdate)
        {
            try
            {
                var orderEntity = _mapper.Map<Order>(orderToUpdate);

                _repository.Order.Update(orderEntity);
                await _repository.Order.SaveChangesAsync();
            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch(AutoMapperMappingException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception($"There was an error updating Order with id: {orderToUpdate?.Id}.", ex);
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            try
            {
                _repository.Order.Delete(id);
                await _repository.Order.SaveChangesAsync();
            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception($"There was an error deleting Order with id: {id}.", ex);
            }
        }

        #region Order Item

        public async Task<IEnumerable<OrderItemDto>?> GetAllOrderItemsAsync(int orderId, int pageSize, int pageNumber)
        {
            try
            {
                var orderItems = await _repository.OrderItem.FindAllByOrderIdAsync(orderId, pageSize, pageNumber);

                if (orderItems is null)
                {
                    return null;
                }

                var orderItemDtos = _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);

                return orderItemDtos;
            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception($"There was an error retrieving Order Items for order with id: {orderId}", ex);
            }
        }

        public async Task<OrderItemDto> GetOrderItemByIdAsync(int orderId, int orderItemId)
        {
            try
            {
                var orderItemEntity = await _repository.OrderItem.FindByIdAsync(orderItemId);

                var orderItemDto = _mapper.Map<OrderItemDto>(orderItemEntity);

                return orderItemDto;
            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception($"There was an error retrieving Order Item with Order id: {orderId} and Order Item id: {orderItemId}.", ex);
            }
        }

        public async Task<OrderItemDto> CreateOrderItemAsync(OrderItemForCreate orderItemToCreate)
        {
            try
            {
                var orderItemEntity = _mapper.Map<OrderDetail>(orderItemToCreate);

                var createdEntity = _repository.OrderItem.Create(orderItemEntity);
                await _repository.SaveChangesAsync();

                var orderItemDto = _mapper.Map<OrderItemDto>(createdEntity);

                return orderItemDto;
            }
            catch(Exception ex)
            {
                throw new Exception("There was an error creating new Order Item.", ex);
            }
        }

        public async Task UpdateOrderItemAsync(OrderItemForUpdate orderItemToUpdate)
        {
            try
            {
                if(orderItemToUpdate is null)
                {
                    throw new ArgumentNullException(nameof(orderItemToUpdate));
                }

                var orderItemDto = _mapper.Map<OrderDetail>(orderItemToUpdate);

                _repository.OrderItem.Update(orderItemDto);
                await _repository.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception($"There was an error updating Order Item with id: {orderItemToUpdate.Id}", ex);
            }
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            try
            {
                _repository.OrderItem.Delete(id);
                await _repository.SaveChangesAsync();
            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new Exception($"There was an error deleting Order Item with id: {id}", ex);
            }
        }

        #endregion
    }
}
