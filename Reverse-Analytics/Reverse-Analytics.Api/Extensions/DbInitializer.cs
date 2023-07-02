using Bogus;
using Microsoft.AspNetCore.Identity;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Enums;
using ReverseAnalytics.Infrastructure.Persistence;
using System.Diagnostics;

namespace Reverse_Analytics.Api.Extensions
{
    internal static class DbInitializer
    {
        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                var identityContext = services.GetRequiredService<ApplicationIdentityDbContext>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                DbSeeder.Initialize(context, identityContext, userManager);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return app;
        }
    }

    internal static class DbSeeder
    {
        private static readonly Faker _faker = new("ru");
        private static readonly Random _random = new();
        private static readonly DateTime minDate = DateTime.Now.AddYears(-2);
        private static readonly DateTime maxDate = DateTime.Now;

        public static void Initialize(ApplicationDbContext context,
            ApplicationIdentityDbContext identityContext,
            UserManager<IdentityUser> userManager)
        {
            try
            {
                CreateProductCategories(context);
                CreateProducts(context);
                CreateCustomers(context);
                CreateSales(context);
                CreateSaleDetails(context);
                CreateSaleDebts(context);
                CreateRefunds(context);
                CreateRefundDetails(context);
                CreateSuppliers(context);
                CreateSupplies(context);
                CreateSupplyDetails(context);
                CreateSupplyDebts(context);
                //CreateInventories(context);
                //CreateInventoryDetails(context);
                CreateRoles(identityContext);
                CreateUsers(identityContext, userManager);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        private static void CreateProductCategories(ApplicationDbContext context)
        {
            if (context.ProductCategories.Any()) return;

            List<ProductCategory> productCategories = new();
            var fakeCategories = new Faker("ru").Commerce.Categories(20);

            for (int i = 0; i < fakeCategories.Length; i++)
            {
                productCategories.Add(
                    new ProductCategory()
                    {
                        CategoryName = fakeCategories[i]
                    });
            }

            context.ProductCategories.AddRange(productCategories);
            context.SaveChanges();
        }

        private static void CreateProducts(ApplicationDbContext context)
        {
            if (context.Products.Any()) return;

            var categories = context.ProductCategories.Select(x => x.Id).ToList();
            var productFaker = new Faker<Product>("ru")
                .RuleFor(x => x.ProductName, f => f.Commerce.ProductName())
                .RuleFor(x => x.ProductCode, f => new Faker().Lorem.Letter(2))
                .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
                .RuleFor(x => x.SupplyPrice, f => f.Random.Decimal(10000, 50000))
                .RuleFor(x => x.SalePrice, f => f.Random.Decimal(15000, 80000))
                .RuleFor(x => x.Volume, f => f.Random.Double(10, 100))
                .RuleFor(x => x.Weight, f => f.Random.Double(15, 150))
                .RuleFor(x => x.UnitOfMeasurement, f => f.Random.Enum<UnitOfMeasurement>())
                .RuleFor(x => x.QuantityInStock, f => f.Random.Int(0, 100))
                .RuleFor(x => x.CategoryId, f => f.Random.ListItem(categories))
                .Generate(100);

            context.Products.AddRange(productFaker);
            context.SaveChanges();
        }

        private static void CreateCustomers(ApplicationDbContext context)
        {
            if (context.Customers.Any()) return;

            var customerFaker = new Faker<Customer>("ru")
                .RuleFor(x => x.FullName, f => f.Person.FullName)
                .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("+998-9#-###-##-##"))
                .RuleFor(x => x.Company, f => f.Company.CompanyName())
                .RuleFor(x => x.Address, f => f.Address.FullAddress())
                .RuleFor(x => x.Balance, f => f.Random.Decimal(0, 500000))
                .RuleFor(x => x.Discount, f => f.Random.Double(0, 50))
                .Generate(30);

            context.Customers.AddRange(customerFaker);
            context.SaveChanges();
        }

        private static void CreateSales(ApplicationDbContext context)
        {
            if (context.Sales.Any()) return;


            var customers = context.Customers.Select(x => x.Id).ToList();
            var fakeSales = new Faker<Sale>("ru")
                .RuleFor(x => x.Receipt, f => f.Random.Guid().ToString()[..8])
                .RuleFor(x => x.Comments, f => f.Commerce.ProductAdjective())
                .RuleFor(x => x.SoldBy, f => f.Person.FirstName)
                .RuleFor(x => x.TotalDue, f => f.Random.Decimal(10000, 5000000))
                .RuleFor(x => x.TotalPaid, f => f.Random.Decimal(5000, 5500000))
                .RuleFor(x => x.TotalDiscount, f => f.Random.Decimal(1000, 500000))
                .RuleFor(x => x.SaleType, f => f.Random.Enum<SaleType>())
                .RuleFor(x => x.SaleDate, f => f.Date.Between(minDate, maxDate))
                .RuleFor(x => x.CustomerId, f => f.Random.ListItem(customers))
                .Generate(100);

            context.Sales.AddRange(fakeSales);
            context.SaveChanges();
        }

        private static void CreateSaleDetails(ApplicationDbContext context)
        {
            if (context.SaleDetails.Any()) return;

            var sales = context.Sales.Select(x => x.Id).ToList();
            var products = context.Products.Select(x => x.Id).ToList();
            var saleDetailsFaker = new Faker<SaleDetail>("ru")
                .RuleFor(x => x.Quantity, f => f.Random.Int(1, 50))
                .RuleFor(x => x.UnitPrice, f => f.Random.Decimal(5000, 50000))
                .RuleFor(x => x.Discount, f => f.Random.Decimal(500, 20000))
                .RuleFor(x => x.SaleId, f => f.Random.ListItem(sales))
                .RuleFor(x => x.ProductId, f => f.Random.ListItem(products))
                .Generate(500);

            context.SaleDetails.AddRange(saleDetailsFaker);
            context.SaveChanges();
        }

        private static void CreateSaleDebts(ApplicationDbContext context)
        {
            if (context.SaleDebts.Any()) return;

            var sales = context.Sales.Select(x => x.Id).ToList();
            var debtsFaker = new Faker<SaleDebt>("ru")
                .RuleFor(x => x.TotalDue, f => f.Random.Decimal(10000, 500000))
                .RuleFor(x => x.DueDate, f => f.Date.Between(minDate, maxDate))
                .RuleFor(x => x.ClosedDate, f => f.Date.Between(minDate, maxDate))
                .RuleFor(x => x.Status, f => f.Random.Enum<DebtStatus>())
                .RuleFor(x => x.SaleId, f => f.Random.ListItem(sales))
                .Generate(25);

            context.SaleDebts.AddRange(debtsFaker);
            context.SaveChanges();
        }

        private static void CreateRefunds(ApplicationDbContext context)
        {
            if (context.Refunds.Any()) return;

            var sales = context.Sales.Select(x => x.Id).ToList();
            var refundFaker = new Faker<Refund>("ru")
                .RuleFor(x => x.RefundDate, f => f.Date.Between(minDate, maxDate))
                .RuleFor(x => x.Reason, f => f.Lorem.Text())
                .RuleFor(x => x.ReceivedBy, f => f.Person.FirstName)
                .RuleFor(x => x.SaleId, f => f.Random.ListItem(sales))
                .Generate(25);

            context.Refunds.AddRange(refundFaker);
            context.SaveChanges();
        }

        private static void CreateRefundDetails(ApplicationDbContext context)
        {
            if (context.RefundDetails.Any()) return;

            var refunds = context.Refunds.Select(x => x.Id).ToList();
            var products = context.Products.Select(x => x.Id).ToList();
            var detailsFaker = new Faker<RefundDetail>("ru")
                .RuleFor(x => x.Quantity, f => f.Random.Int(1, 10))
                .RuleFor(x => x.RefundId, f => f.Random.ListItem(refunds))
                .RuleFor(x => x.ProductId, f => f.Random.ListItem(products))
                .Generate(150);

            context.RefundDetails.AddRange(detailsFaker);
            context.SaveChanges();
        }

        private static void CreateSuppliers(ApplicationDbContext context)
        {
            if (context.Suppliers.Any()) return;

            var suppliersFaker = new Faker<Supplier>("ru")
                .RuleFor(x => x.FullName, f => f.Person.FullName)
                .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("+998-9#-###-##-##"))
                .RuleFor(x => x.Company, f => f.Company.CompanyName())
                .RuleFor(x => x.Balance, f => f.Random.Decimal(0, 500000))
                .Generate(30);

            context.Suppliers.AddRange(suppliersFaker);
            context.SaveChanges();
        }

        private static void CreateSupplies(ApplicationDbContext context)
        {
            if (context.Supplies.Any()) return;

            var suppliers = context.Suppliers.Select(x => x.Id).ToList();
            var suppliesFaker = new Faker<Supply>("ru")
                .RuleFor(x => x.ReceivedBy, f => f.Person.FirstName)
                .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
                .RuleFor(x => x.TotalDue, f => f.Random.Decimal(100000, 5000000))
                .RuleFor(x => x.TotalPaid, f => f.Random.Decimal(50000, 5500000))
                .RuleFor(x => x.SupplyDate, f => f.Date.Between(minDate, maxDate))
                .RuleFor(x => x.SupplierId, f => f.Random.ListItem(suppliers))
                .Generate(100);

            context.Supplies.AddRange(suppliesFaker);
            context.SaveChanges();
        }

        private static void CreateSupplyDetails(ApplicationDbContext context)
        {
            if (context.SupplyDetails.Any()) return;

            var supplies = context.Supplies.Select(x => x.Id).ToList();
            var products = context.Products.Select(x => x.Id).ToList();
            var detailsFaker = new Faker<SupplyDetail>("ru")
                .RuleFor(x => x.Quantity, f => f.Random.Int(1, 50))
                .RuleFor(x => x.UnitPrice, f => f.Random.Decimal(5000, 50000))
                .RuleFor(x => x.SupplyId, f => f.Random.ListItem(supplies))
                .RuleFor(x => x.ProductId, f => f.Random.ListItem(products))
                .Generate(350);

            context.SupplyDetails.AddRange(detailsFaker);
            context.SaveChanges();
        }

        private static void CreateSupplyDebts(ApplicationDbContext context)
        {
            if (context.SupplyDebts.Any()) return;

            var supplies = context.Supplies.Select(x => x.Id).ToList();
            var debtsFaker = new Faker<SupplyDebt>("ru")
                .RuleFor(x => x.TotalDue, f => f.Random.Decimal(10000, 500000))
                .RuleFor(x => x.DueDate, f => f.Date.Between(minDate, maxDate))
                .RuleFor(x => x.ClosedDate, f => f.Date.Between(minDate, maxDate))
                .RuleFor(x => x.Status, f => f.Random.Enum<DebtStatus>())
                .RuleFor(x => x.SupplyId, f => f.Random.ListItem(supplies))
                .Generate(25);

            context.SupplyDebts.AddRange(debtsFaker);
            context.SaveChanges();
        }

        private static void CreateRoles(ApplicationIdentityDbContext context)
        {
            if (context.Roles.Any()) return;

            List<IdentityRole> roles = new();

            roles.Add(new IdentityRole
            {
                Name = "Visitor",
                NormalizedName = "VISITOR"
            });
            roles.Add(new IdentityRole
            {
                Name = "Regular",
                NormalizedName = "REGULAR"
            });
            roles.Add(new IdentityRole
            {
                Name = "Accountant",
                NormalizedName = "ACCOUNTANT"
            });
            roles.Add(new IdentityRole
            {
                Name = "Manager",
                NormalizedName = "MANAGER"
            });
            roles.Add(new IdentityRole
            {
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            });

            context.Roles.AddRange(roles);
            context.SaveChanges();
        }

        private static void CreateUsers(ApplicationIdentityDbContext context, UserManager<IdentityUser> userManager)
        {
            if (context.Users.Any()) return;

            var roles = context.Roles.ToList();

            for (int i = 0; i < 20; i++)
            {
                var role = roles.ElementAt(_random.Next(0, roles.Count));

                var user = new IdentityUser
                {
                    UserName = _faker.Internet.UserName(),
                };

                userManager.CreateAsync(user, $"qwerty{i}").Wait();
                userManager.AddToRoleAsync(user, role.Name).Wait();
            }
        }
    }
}
