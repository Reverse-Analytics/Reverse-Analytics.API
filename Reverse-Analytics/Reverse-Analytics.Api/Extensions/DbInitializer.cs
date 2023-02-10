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
            ArgumentNullException.ThrowIfNull(app, nameof(app));

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

    internal class DbSeeder
    {
        private static readonly Faker _faker = new();
        private static readonly Random _random = new();

        public static void Initialize(ApplicationDbContext context, ApplicationIdentityDbContext identityContext, UserManager<IdentityUser> userManager)
        {
            try
            {
                //CreateProductCategories(context);
                //CreateProducts(context);
                //CreateCustomers(context);
                //CreateSales(context);
                //CreateSaleDetails(context);
                //CreateSaleDebts(context);
                //CreateSuppliers(context);
                //CreateSupplyDebts(context);
                //CreateSupplies(context);
                //CreateSupplyDetails(context);
                //CreateInventories(context);
                //CreateInventoryDetails(context);
                //CreateRoles(identityContext);
                //CreateUsers(identityContext, userManager);
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
            var fakeCategories = _faker.Commerce.Categories(25);

            for (int i = 0; i < 25; i++)
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

            var categories = context.ProductCategories.ToList();
            List<Product> products = new();

            foreach (var category in categories)
            {
                int numberOfProducts = _random.Next(1, 25);

                for (int i = 0; i < numberOfProducts; i++)
                {
                    products.Add(
                        new Product()
                        {
                            ProductCode = _faker.Commerce.ProductAdjective(),
                            ProductName = _faker.Commerce.ProductName(),
                            Volume = (double)(_random.NextDouble() * _random.Next(1, 500)),
                            Weight = (double)(_random.NextDouble() * _random.Next(1, 600)),
                            SupplyPrice = Math.Round((decimal)_random.NextDouble() * 500, 2),
                            SalePrice = Math.Round((decimal)_random.NextDouble() * 800, 2),
                            UnitOfMeasurement = _faker.Random.Enum<UnitOfMeasurement>(),
                            CategoryId = category.Id
                        });
                }
            }

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void CreateCustomers(ApplicationDbContext context)
        {
            if (context.Customers.Any()) return;

            // Customers
            List<Customer> customers = new(50);

            for (int i = 0; i < 50; i++)
            {
                customers.Add(new Customer()
                {
                    FullName = _faker.Person.FullName,
                    CompanyName = _faker.Company.CompanyName(),
                    Address = _faker.Address.FullAddress(),
                    PhoneNumber = _faker.Phone.PhoneNumber("(###) ##-###-##-##"),
                    Balance = _faker.Finance.Amount(0, 10000000),
                    Discount = _faker.Random.Double(0, 100),
                    IsActive = _faker.Random.Bool()
                });
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        private static void CreateSales(ApplicationDbContext context)
        {
            if (context.Sales.Any()) return;

            var customers = context.Customers.ToList();
            List<Sale> sales = new();

            foreach (var customer in customers)
            {
                int salesCount = _random.Next(5, 25);

                for (int i = 0; i < salesCount; i++)
                {
                    var totalDue = decimal.Round(_faker.Random.Decimal(10, 5000), 2);
                    var totalPaid = decimal.Round(_faker.Random.Decimal(0, totalDue), 2);
                    var discountPercentage = _faker.Random.Double(0, 100);
                    var discountTotal = decimal.Round((totalDue * (decimal)discountPercentage) / 100, 2);

                    sales.Add(
                        new Sale()
                        {
                            TotalDue = totalDue,
                            TotalPaid = totalPaid,
                            TransactionDate = _faker.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now),
                            Comments = _faker.Lorem.Sentence(),
                            Status = _faker.Random.Enum<TransactionStatusType>(),
                            Receipt = _faker.Random.Guid().ToString(),
                            Discount = discountTotal,
                            SaleType = _faker.Random.Enum<SaleType>(),
                            CustomerId = customer.Id
                        });
                }
            }

            context.Sales.AddRange(sales);
            context.SaveChanges();
        }

        private static void CreateSaleDetails(ApplicationDbContext context)
        {
            if (context.SaleDetails.Any()) return;

            var sales = context.Sales.ToList();
            var products = context.Products.ToList();
            List<SaleDetail> orderItems = new();

            foreach (var sale in sales)
            {
                var orderItemsCount = _random.Next(1, 15);

                for (int i = 0; i < orderItemsCount; i++)
                {
                    orderItems.Add(
                        new SaleDetail()
                        {
                            Quantity = _random.Next(1, 20),
                            UnitPrice = decimal.Round(_faker.Random.Decimal(5, 500), 2),
                            SaleId = sale.Id,
                            ProductId = products[_random.Next(0, products.Count)]?.Id ?? 1
                        });
                }
            }

            context.SaleDetails.AddRange(orderItems);
            context.SaveChanges();
        }

        private static void CreateSaleDebts(ApplicationDbContext context)
        {
            if (context.Debts.Any()) return;

            List<Debt> debts = new List<Debt>();
            var sales = context.Sales.ToList();

            foreach (var sale in sales)
            {
                if (_faker.Random.Bool())
                {
                    var totalAmount = sale.TotalDue - sale.TotalPaid;
                    var remainedAmount = _faker.Random.Decimal(0, totalAmount);

                    DateTime? paidDate = remainedAmount == 0 ? _faker.Date.Between(sale.TransactionDate, DateTime.Now.AddMonths(5)) : null;
                    DebtStatus status = remainedAmount == 0 ? DebtStatus.Closed : DebtStatus.PaymentRequired;

                    debts.Add(new Debt()
                    {
                        TotalAmount = totalAmount,
                        Remained = remainedAmount,
                        PaidDate = paidDate,
                        DueDate = _faker.Date.Between(sale.TransactionDate, DateTime.Now.AddMonths(5)),
                        Status = status,
                        TransactionId = sale.Id
                    });
                }
            }

            context.Debts.AddRange(debts);
            context.SaveChanges();
        }

        private static void CreateSuppliers(ApplicationDbContext context)
        {
            if (context.Suppliers.Any()) return;

            List<Supplier> suppliers = new List<Supplier>();

            for (int i = 0; i < 25; i++)
            {
                suppliers.Add(
                    new Supplier()
                    {
                        FullName = _faker.Person.FullName,
                        Address = _faker.Address.FullAddress(),
                        PhoneNumber = _faker.Phone.PhoneNumber("(###) ##-###-##-##"),
                        CompanyName = _faker.Company.CompanyName(),
                        Balance = _faker.Random.Decimal(0, 10000),
                        IsActive = _faker.Random.Bool()
                    });
            }

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }

        private static void CreateSupplies(ApplicationDbContext context)
        {
            if (context.Supplies.Any()) return;

            var suppliers = context.Suppliers.ToList();
            List<Supply> supplies = new();

            foreach (var supplier in suppliers)
            {
                int suppliesCount = _random.Next(5, 25);

                for (int i = 0; i < suppliesCount; i++)
                {
                    var totalDue = decimal.Round(_faker.Random.Decimal(10, 5000), 2);
                    var totalPaid = decimal.Round(_faker.Random.Decimal(100, totalDue), 2);

                    supplies.Add(
                        new Supply()
                        {
                            TotalDue = totalDue,
                            TotalPaid = totalPaid,
                            TransactionDate = _faker.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now),
                            Comments = _faker.Lorem.Sentence(),
                            Status = _faker.Random.Enum<TransactionStatusType>(),
                            ReceivedBy = _faker.Name.FirstName(),
                            SupplierId = supplier.Id
                        });
                }
            }

            context.Supplies.AddRange(supplies);
            context.SaveChanges();
        }

        private static void CreateSupplyDetails(ApplicationDbContext context)
        {
            if (context.SupplyDetails.Any()) return;

            var supplies = context.Supplies.ToList();
            var products = context.Products.ToList();
            List<SupplyDetail> supplyDetails = new List<SupplyDetail>();

            foreach (var supply in supplies)
            {
                int suppliesCount = _random.Next(1, 15);

                for (int i = 0; i < suppliesCount; i++)
                {
                    supplyDetails.Add(new SupplyDetail()
                    {
                        Quantity = _random.Next(1, 20),
                        UnitPrice = decimal.Round(_faker.Random.Decimal(5, 500), 2),

                        SupplyId = supply.Id,
                        ProductId = products[_random.Next(0, products.Count)]?.Id ?? 1
                    });
                }
            }

            context.SupplyDetails.AddRange(supplyDetails);
            context.SaveChanges();
        }

        private static void CreateSupplyDebts(ApplicationDbContext context)
        {
            if (context.Debts.Any()) return;

            List<Debt> debts = new List<Debt>();
            var supplies = context.Supplies.ToList();

            foreach (var supply in supplies)
            {
                if (_faker.Random.Bool())
                {
                    var totalAmount = supply.TotalDue - supply.TotalPaid;
                    var remainedAmount = _faker.Random.Decimal(0, totalAmount);

                    DateTime? paidDate = remainedAmount == 0 ? _faker.Date.Between(supply.TransactionDate, DateTime.Now.AddMonths(5)) : null;
                    DebtStatus status = remainedAmount == 0 ? DebtStatus.Closed : DebtStatus.PaymentRequired;

                    debts.Add(new Debt()
                    {
                        TotalAmount = totalAmount,
                        Remained = remainedAmount,
                        PaidDate = paidDate,
                        DueDate = _faker.Date.Between(supply.TransactionDate, DateTime.Now.AddMonths(5)),
                        Status = status
                    });
                }
            }

            context.Debts.AddRange(debts);
            context.SaveChanges();
        }

        private static void CreateInventories(ApplicationDbContext context)
        {
            if (context.Inventories.Any()) return;

            List<Inventory> inventories = new List<Inventory>();

            for (int i = 0; i < 5; i++)
            {
                inventories.Add(new Inventory(_faker.Lorem.Word()));
            }

            context.Inventories.AddRange(inventories);
        }

        private static void CreateInventoryDetails(ApplicationDbContext context)
        {
            if (context.InventoryDetails.Any()) return;

            var inventories = context.Inventories.ToList();
            var products = context.Products.ToList();
            List<InventoryDetail> inventoryDetails = new List<InventoryDetail>();

            foreach (var inventory in inventories)
            {
                var randomProducts = GetRandomProducts(products);

                foreach (var product in randomProducts)
                {
                    inventoryDetails.Add(new InventoryDetail()
                    {
                        InventoryId = inventory.Id,
                        ProductId = product.Id,
                        ProductsRemained = _faker.Random.Double(0, 300)
                    });
                }
            }

            context.InventoryDetails.AddRange(inventoryDetails);
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

        private static List<Product> GetRandomProducts(List<Product> products)
        {
            var half = products.Count / 2;
            var start = _faker.Random.Int(0, half);

            return new List<Product>(products.Skip(start).Take(half));
        }
    }
}
