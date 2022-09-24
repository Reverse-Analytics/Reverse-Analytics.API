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
        private static Faker _faker = new Faker();

        public static void Initialize(ApplicationDbContext context)
        {
            try
            {
                CreateCustomers(context);
                CreateCustomerPhones(context);
            }
            catch (Exception)
            {
            }
        }

        private static void CreateCustomers(ApplicationDbContext context)
        {
            // Customers
            List<Customer> customers = new List<Customer>();

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
            List<CustomerPhone> customerPhones = new List<CustomerPhone>();

            foreach(var customer in customers)
            {
                for(int i = 0; i < 3; i++)
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
    }
}
