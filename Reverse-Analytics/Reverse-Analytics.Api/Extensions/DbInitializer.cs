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
                CreateProductCategories(context);
                CreateProducts(context);
                CreateCustomers(context);
                CreateCustomerPhones(context);
                CreateCustomerDebts(context);
                CreateOrders(context);
                CreateOrderItems(context);
                CreateSuppliers(context);
                CreateSupplierPhones(context);
                CreateSupplierDebts(context);
                CreateSupplies(context);
                CreateSupplyDetails(context);
                CreateRoles(identityContext);
                CreateUsers(identityContext, userManager);
            }
            catch (Exception)
            {
            }
        }

        private static void CreateCustomers(ApplicationDbContext context)
        {
            if (context.Customers.Any()) return;

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
            if (context.CustomerPhones.Any()) return;

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
            if (context.CustomerDebts.Any()) return;

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

        private static void CreateProductCategories(ApplicationDbContext context)
        {
            if (context.ProductCategories.Any()) return;

            List<ProductCategory> productCategories = new();
            var fakeCategories = _faker.Commerce.Categories(50);

            for(int i = 0; i < 50; i++)
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

            foreach(var category in categories)
            {
                int numberOfProducts = _random.Next(1, 15);

                for(int i = 0; i < numberOfProducts; i++)
                {
                    products.Add(
                        new Product()
                        {
                            ProductName = _faker.Commerce.ProductName(),
                            Volume = (double)(_random.NextDouble() * _random.Next(1, 20)),
                            Weight = (double)(_random.NextDouble() * _random.Next(1, 100)),
                            SupplyPrice = Math.Round((decimal)_random.NextDouble() * 500, 2),
                            SalePrice = Math.Round((decimal)_random.NextDouble() * 800, 2),
                            CategoryId = category.Id
                        });
                }
            }

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void CreateOrders(ApplicationDbContext context)
        {
            if (context.Orders.Any()) return;

            var customers = context.Customers.ToList();
            List<Order> orders = new List<Order>();
            var enumValues = Enum.GetValues(typeof(OrderStatus));

            foreach(var customer in customers)
            {
                int ordersCount = _random.Next(1, 25);

                for(int i = 0; i < ordersCount; i++)
                {
                    var totalDue = decimal.Round(_faker.Random.Decimal(10, 5000), 2);
                    var discountPercentage = decimal.Round(_faker.Random.Decimal(0, 100), 2);
                    var discountTotal = decimal.Round((totalDue * discountPercentage) / 100, 2);
                    OrderStatus status = (OrderStatus)enumValues.GetValue(_random.Next(enumValues.Length));

                    orders.Add(
                        new Order()
                        {
                            TotalDue = totalDue,
                            DiscountPercentage = discountPercentage,
                            DiscountTotal = discountTotal,
                            Comment = _faker.Lorem.Sentence(null, 50),
                            OrderDate = _faker.Date.Between(new DateTime(DateTime.Now.Year, 1, 1), DateTime.Now),
                            Status = status,
                            CustomerId = customer.Id
                        });
                }
            }

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }

        private static void CreateOrderItems(ApplicationDbContext context)
        {
            if (context.OrderItems.Any()) return;

            var orders = context.Orders.ToList();
            var products = context.Products.ToList();
            List<OrderDetail> orderItems = new();

            foreach(var order in orders)
            {
                var orderItemsCount = _random.Next(1, 30);
             
                for(int i = 0; i < orderItemsCount; i++)
                {
                    orderItems.Add(
                        new OrderDetail()
                        {
                            Quantity = _random.Next(1, 20),
                            UnitPrice = decimal.Round(_faker.Random.Decimal(5, 500), 2),
                            UnitPriceDiscount = decimal.Round(_faker.Random.Decimal(0, 100), 2),
                            OrderId = order.Id,
                            ProductId = products[_random.Next(0, products.Count)]?.Id ?? 1
                        });
                } 
            }

            context.OrderItems.AddRange(orderItems);
            context.SaveChanges();
        }

        private static void CreateSuppliers(ApplicationDbContext context)
        {
            if (context.Suppliers.Any()) return;

            List<Supplier> suppliers = new List<Supplier>();

            for(int i = 0; i < 500; i++)
            {
                suppliers.Add(new Supplier()
                {
                    FirstName = _faker.Name.FirstName(),
                    LastName = _faker.Name.LastName(),
                    CompanyName = _faker.Company.CompanyName()
                });
            }

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }

        private static void CreateSupplierPhones(ApplicationDbContext context)
        {
            if (context.SupplierPhones.Any()) return;

            var suppliers = context.Suppliers.ToList();
            List<SupplierPhone> supplierPhones = new();

            foreach (var supplier in suppliers)
            {
                int numberOfPhones = _random.Next(0, 5);

                for (int i = 0; i < numberOfPhones; i++)
                {
                    supplierPhones.Add(
                        new SupplierPhone()
                        {
                            SupplierId = supplier.Id,
                            PhoneNumber = _faker.Phone.PhoneNumber()
                        });
                }
            }

            context.SupplierPhones.AddRange(supplierPhones);
            context.SaveChanges();
        }

        private static void CreateSupplierDebts(ApplicationDbContext context)
        {
            if (context.SupplierDebts.Any()) return;

            var suppliers = context.Suppliers.ToList();
            List<SupplierDebt> supplierDebts = new();

            foreach (var supplier in suppliers)
            {
                int numberOfDebts = _random.Next(0, 10);

                for (int i = 0; i < numberOfDebts; i++)
                {
                    supplierDebts.Add(
                        new SupplierDebt()
                        {
                            SupplierId = supplier.Id,
                            Amount = _faker.Finance.Amount(),
                            DebtDate = _faker.Date.Between(DateTime.Now.AddMonths(-12), DateTime.Now),
                            DueDate = _faker.Date.Between(DateTime.Now.AddMonths(2), DateTime.Now.AddMonths(12))
                        });
                }
            }

            context.SupplierDebts.AddRange(supplierDebts);
            context.SaveChanges();
        }

        private static void CreateSupplies(ApplicationDbContext context)
        {
            if (context.Supplies.Any()) return;

            var suppliers = context.Suppliers.ToList();
            List<Supply> supplies = new();

            foreach (var supplier in suppliers)
            {
                int suppliesCount = _random.Next(1, 25);

                for (int i = 0; i < suppliesCount; i++)
                {
                    var totalDue = decimal.Round(_faker.Random.Decimal(10, 5000), 2);
                    var paidAmount = decimal.Round(_faker.Random.Decimal(100, totalDue), 2);
                    var debtAmount = totalDue - paidAmount;

                    supplies.Add(
                        new Supply()
                        {
                            TotalDue = totalDue,
                            PaymentAmount = paidAmount,
                            DebtAmount = debtAmount,
                            ReceivedBy = _faker.Name.FirstName(),
                            PurchaseDate = _faker.Date.Between(new DateTime(DateTime.Now.Year, 1, 1), DateTime.Now),
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

            foreach(var supply in supplies)
            {
                int suppliesCount = _random.Next(1, 25);

                for(int i = 0; i < suppliesCount; i++)
                {
                    supplyDetails.Add(new SupplyDetail()
                    {
                        Quantity = _random.Next(1, 20),
                        UnitPrice = decimal.Round(_faker.Random.Decimal(5, 500), 2),
                        UnitPriceDiscount = decimal.Round(_faker.Random.Decimal(0, 100), 2),
                        SupplyId = supply.Id,
                        ProductId = products[_random.Next(0, products.Count)]?.Id ?? 1
                    });
                }
            }

            context.SupplyDetails.AddRange(supplyDetails);
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

            for(int i = 0; i < 20; i++)
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
