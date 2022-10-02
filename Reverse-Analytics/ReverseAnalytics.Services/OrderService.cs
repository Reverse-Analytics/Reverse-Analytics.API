using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Order;
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
    }
}
