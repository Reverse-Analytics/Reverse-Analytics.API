using Bogus;
using ReverseAnalytics.Domain.Entities;

namespace Reverse_Analytics.Api.Fakers
{
    public class CategoryFaker
    {
        public Faker<ProductCategory> GetFaker()
        {
            var faker = new Faker<ProductCategory>()
                .RuleFor(x => x.Name, faker => faker.Commerce.Categories(1).FirstOrDefault());

            return faker;
        }
    }
}
