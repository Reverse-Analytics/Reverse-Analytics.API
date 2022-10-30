using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class SupplierPhoneRepository : RepositoryBase<SupplierPhone>, ISupplierPhoneRepository
    {
        public SupplierPhoneRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
