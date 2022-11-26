using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class InventoryDetailRepository : RepositoryBase<InventoryDetail>, IInventoryDetailRepository
    {
        public InventoryDetailRepository(ApplicationDbContext context) 
            : base(context)
        {
        }
    }
}
