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
    }
}
