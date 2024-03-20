using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.TestDataCreator;

public class DatabaseSeeder(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;
    private readonly Fakers _faker = new();

    public void Seed()
    {
        GenerateProductCategories();
        GenerateProducts();
        GenerateCustomers();
        GenerateSales();
        GenerateSaleItems();
        GenerateSuppliers();
        GenerateSupplies();
        GenerateSupplyItems();
        GenerateTransactions();
    }

    private void GenerateProductCategories()
    {
        HashSet<string> categoryNames = [];

        // try to generate only unique values
        for (int i = 0; i < 100; i++)
        {
            int attempts = 0;
            var category = _faker.ProductCategory().Generate();

            while (categoryNames.Contains(category.Name) && attempts < 100)
            {
                category = _faker.ProductCategory().Generate();
                attempts++;
            }

            _context.ProductCategories.Add(category);
        }

        _context.SaveChanges();
    }

    private void GenerateProducts()
    {
        HashSet<string> productNames = [];
        var categories = _context.ProductCategories.Select(x => x.Id).ToArray();

        // try to generate only unique values
        for (int i = 0; i < 1_500; i++)
        {
            int attempts = 0;
            var product = _faker.Product(categories).Generate();

            while (productNames.Contains(product.Name) && attempts < 100)
            {
                product = _faker.Product(categories).Generate();
                attempts++;
            }

            _context.Products.Add(product);
        }

        _context.SaveChanges();
    }

    private void GenerateCustomers()
    {
        var customers = _faker.Customer().Generate(250);

        _context.Customers.AddRange(customers);
        _context.SaveChanges();
    }

    private void GenerateSales()
    {
        var customers = _context.Customers.Select(x => x.Id).ToArray();
        var sales = _faker.Sale(customers).Generate(7_500);

        _context.Sales.AddRange(sales);
        _context.SaveChanges();
    }

    private void GenerateSaleItems()
    {
        var sales = _context.Sales.Select(x => x.Id).ToArray();
        var products = _context.Products.Select(x => x.Id).ToArray();
        var saleItems = _faker.SaleItems(sales, products).Generate(100_000);

        _context.SaleItems.AddRange(saleItems);
        _context.SaveChanges();
    }

    private void GenerateSuppliers()
    {
        var suppliers = _faker.Supplier().Generate(150);

        _context.Suppliers.AddRange(suppliers);
        _context.SaveChanges();
    }

    private void GenerateSupplies()
    {
        var suppliers = _context.Suppliers.Select(x => x.Id).ToArray();
        var suppplies = _faker.Supply(suppliers).Generate(5_000);

        _context.Supplies.AddRange(suppplies);
        _context.SaveChanges();
    }

    private void GenerateSupplyItems()
    {
        var supplies = _context.Supplies.Select(x => x.Id).ToArray();
        var products = _context.Products.Select(x => x.Id).ToArray();
        var supplyItems = _faker.SupplyItems(supplies, products).Generate(75_000);

        _context.SupplyItems.AddRange(supplyItems);
        _context.SaveChanges();
    }

    private void GenerateTransactions()
    {
        var sales = _context.Sales.ToArray();
        var supplies = _context.Supplies.ToArray();

        foreach (var sale in sales)
        {
            var transaction = new Transaction
            {
                Date = sale.Date,
                Amount = sale.GetTransactionAmount(),
                Source = sale.TransactionSource,
                Type = sale.TransactionType,
                SourceId = sale.GetTransactionSourceId()
            };

            _context.Transaction.Add(transaction);
        }

        foreach (var supply in supplies)
        {
            var transaction = new Transaction
            {
                Date = supply.Date,
                Amount = supply.GetTransactionAmount(),
                Source = supply.TransactionSource,
                Type = supply.TransactionType,
                SourceId = supply.GetTransactionSourceId()
            };

            _context.Transaction.Add(transaction);
        }

        _context.SaveChanges();
    }
}
