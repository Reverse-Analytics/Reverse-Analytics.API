using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class OrderItemRepository : RepositoryBase<OrderDetail>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<OrderDetail>> FindAllByOrderIdAsync(int orderId, int pageSize = 0, int pageNumber = 0)
        {
            if(pageSize > 0)
            {
                return await _context.OrderItems
                    .Where(oi => oi.OrderId == orderId)
                    .AsNoTracking()
                    .Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .ToListAsync();
            }

            return await _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
