using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.QueryParameters;
using ReverseAnalytics.Infrastructure.Extensions;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories;

public class TransactionRepository(ApplicationDbContext context) : RepositoryBase<Transaction>(context), ITransactionRepository
{
    public async Task<PaginatedList<Transaction>> FindAllAsync(TransactionQueryParameters queryParameters)
    {
        ArgumentNullException.ThrowIfNull(queryParameters);

        var query = _context.Transaction.AsQueryable();

        if (queryParameters.Source.HasValue)
        {
            query = query.Where(x => x.Source == queryParameters.Source);
        }

        if (queryParameters.Amount.HasValue)
        {
            query = query.Where(x => x.Amount == queryParameters.Amount);
        }

        if (queryParameters.Date.HasValue)
        {
            query = query.Where(x => x.Date == queryParameters.Date);
        }

        if (queryParameters.Type.HasValue)
        {
            query = query.Where(x => x.Type == queryParameters.Type);
        }

        return await query.ToPaginatedListAsync(queryParameters.PageNumber, queryParameters.PageSize);
    }
}
