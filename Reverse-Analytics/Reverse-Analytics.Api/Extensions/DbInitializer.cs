using Bogus;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Infrastructure.Persistence;

namespace Reverse_Analytics.Api.Extensions
{
    internal static class DbInitializer
    {
        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                if (context.Customers.Any())
                {
                    return app;
                }

                DbSeeder.Initialize(context);
            }
            catch (Exception ex)
            {
            }

            return app;
        }
    }

    internal class DbSeeder
    {
        private static readonly Faker _faker = new();
        private static readonly Random _random = new();

        public static void Initialize(ApplicationDbContext context)
        {
            try
            {
                CreateCustomers(context);
                CreateCustomerPhones(context);
                CreateCustomerDebts(context);
            }
            catch (Exception)
            {
            }
        }

        private static void CreateCustomers(ApplicationDbContext context)
        {
            // Customers
            List<Customer> customers = new();

            for(int i = 0; i < 500; i++)
            {
                customers.Add(
                    new Customer()
                    {
                        FirstName = _faker.Name.FirstName(),
                        LastName = _faker.Name.LastName(),
                        Address = _faker.Address.City(),
                        CompanyName = _faker.Company.CompanyName()
                    });
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        private static void CreateCustomerPhones(ApplicationDbContext context)
        {
            var customers = context.Customers.ToList();
            List<CustomerPhone> customerPhones = new();

            foreach(var customer in customers)
            {
                int numberOfPhones = _random.Next(0, 5);

                for(int i = 0; i < numberOfPhones; i++)
                {
                    customerPhones.Add(
                        new CustomerPhone()
                        {
                            CustomerId = customer.Id,
                            PhoneNumber = _faker.Phone.PhoneNumber()
                        });
                }
            }

            context.CustomerPhones.AddRange(customerPhones);
            context.SaveChanges();
        }

        private static void CreateCustomerDebts(ApplicationDbContext context)
        {
            var customers = context.Customers.ToList();
            List<CustomerDebt> customerDebts = new();

            foreach(var customer in customers)
            {
                int numberOfDebts = _random.Next(0, 10);

                for(int i = 0; i < numberOfDebts; i++)
                {
                    customerDebts.Add(
                        new CustomerDebt()
                        {
                            CustomerId = customer.Id,
                            Amount = _faker.Finance.Amount(),
                            DebtDate = _faker.Date.Between(DateTime.Now.AddMonths(-12), DateTime.Now),
                            DueDate = _faker.Date.Between(DateTime.Now.AddMonths(2), DateTime.Now.AddMonths(12))
                        });
                }
            }

            context.CustomerDebts.AddRange(customerDebts);
            context.SaveChanges();
        }
    }
}
