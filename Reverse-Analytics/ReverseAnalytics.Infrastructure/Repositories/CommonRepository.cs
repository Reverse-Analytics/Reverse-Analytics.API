using ReverseAnalytics.Domain.Interfaces.Repositories;

namespace ReverseAnalytics.Infrastructure.Repositories;

public class CommonRepository : ICommonRepository
{
    public ICustomerRepository Customer => throw new NotImplementedException();

    public IProductCategoryRepository ProductCategory => throw new NotImplementedException();

    public IProductRepository Product => throw new NotImplementedException();

    public ISaleItemRepository SaleItem => throw new NotImplementedException();

    public ISaleRepository Sale => throw new NotImplementedException();

    public ISupplierRepository Supplier => throw new NotImplementedException();

    public ISupplyItemRepository SupplyItem => throw new NotImplementedException();

    public ISupplyRepository Supply => throw new NotImplementedException();

    public ITransactionRepository Transaction => throw new NotImplementedException();

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
