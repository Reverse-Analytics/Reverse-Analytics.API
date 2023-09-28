using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.Interfaces.Repositories
{
    public interface IRefundRepository : IRepositoryBase<Refund>
    {
        Task<IEnumerable<Refund>> FindRefundsAsync();
    }
}
