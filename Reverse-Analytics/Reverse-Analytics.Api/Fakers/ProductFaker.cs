using Bogus;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Enums;

namespace Reverse_Analytics.Api.Fakers
{
    public class ProductFaker
    {
        public Faker<Product> GetFaker(int[] categoryIds)
        {
            var faker = new Faker();
            var salePrice = faker.Random.Decimal(10_000, 250_000);
            var supplyPrice = faker.Random.Decimal(10_000, salePrice);

            var productFaker = new Faker<Product>()
                .RuleFor(x => x.Name, faker => faker.Commerce.ProductName())
                .RuleFor(x => x.Code, faker => faker.Commerce.ProductAdjective())
                .RuleFor(x => x.Description, faker => faker.Commerce.ProductDescription())
                .RuleFor(x => x.SalePrice, salePrice)
                .RuleFor(x => x.SupplyPrice, supplyPrice)
                .RuleFor(x => x.Volume, faker.Random.Double(10, 250))
                .RuleFor(x => x.Weight, faker.Random.Double(10, 250))
                .RuleFor(x => x.UnitOfMeasurement, faker.Random.Enum<UnitOfMeasurement>())
                .RuleFor(x => x.CategoryId, faker.Random.ArrayElement(categoryIds));

            return productFaker;
        }
    }
}
