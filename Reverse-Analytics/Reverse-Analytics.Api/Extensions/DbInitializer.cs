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
                //CreateCustomerAddress(context);
                //CreateCustomerPhones(context);
                //CreateCustomerDebts(context);
                //CreateSales(context);
                //CreateSaleDetails(context);
                //CreateSuppliers(context);
                //CreateSupplierAddresses(context);
                //CreateSupplierPhones(context);
                //CreateSupplierDebts(context);
                //CreateSupplies(context);
                //CreateSupplyDetails(context);
                //CreateInventories(context);
                //CreateInventoryDetails(context);
                //CreateRoles(identityContext);
                //CreateUsers(identityContext, userManager);
            }
            catch (Exception)
            {
            }
        }


        private static void CreateProductCategories(ApplicationDbContext context)
        {
            if (context.ProductCategories.Any()) return;

            List<ProductCategory> productCategories = new();
            var fakeCategories = _faker.Commerce.Categories(50);

            for (int i = 0; i < 50; i++)
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

        private static void CreateCustomers(ApplicationDbContext context)
        {
            if (context.Customers.Any()) return;

            // Customers
            List<Customer> customers = new();

            for (int i = 0; i < 500; i++)
            {
                customers.Add(
                    new Customer()
                    {
                        FullName = _faker.Person.FullName,
                        CompanyName = _faker.Company.CompanyName(),
                        Balance = _faker.Random.Decimal(0, 10000),
                        IsActive = true,
                        ContactPerson = _faker.Person.FullName,
                        ContactPersonPhone = _faker.Phone.PhoneNumber()
                    });
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        private static void CreateCustomerAddress(ApplicationDbContext context)
        {
            if (context.Addresses.Any()) return;

            var customers = context.Customers.ToList();
            List<Address> customerAddresses = new();

            foreach (var customer in customers)
            {
                int numberOfPhones = _random.Next(0, 5);

                for (int i = 0; i < numberOfPhones; i++)
                {
                    customerAddresses.Add(
                        new Address()
                        {
                            PersonId = customer.Id,
                            AddressDetails = _faker.Address.FullAddress(),
                            AddressLandMark = _faker.Address.StreetAddress(),
                            Latitude = _faker.Address.Latitude(),
                            Longtitude = _faker.Address.Longitude(),
                        });
                }
            }

            context.Addresses.AddRange(customerAddresses);
            context.SaveChanges();
        }

        private static void CreateCustomerPhones(ApplicationDbContext context)
        {
            var customers = context.Customers.ToList();
            List<Phone> customersPhones = new();

            foreach (var customer in customers)
            {
                int numberOfPhones = _random.Next(0, 5);

                for (int i = 0; i < numberOfPhones; i++)
                {
                    customersPhones.Add(
                        new Phone()
                        {
                            PersonId = customer.Id,
                            PhoneNumber = _faker.Phone.PhoneNumber()
                        });
                }
            }

            context.Phones.AddRange(customersPhones);
            context.SaveChanges();
        }

        private static void CreateCustomerDebts(ApplicationDbContext context)
        {
            var customers = context.Customers.ToList();
            List<Debt> supplierDebts = new();

            foreach (var customer in customers)
            {
                int numberOfDebts = _random.Next(0, 10);

                for (int i = 0; i < numberOfDebts; i++)
                {
                    supplierDebts.Add(
                        new Debt()
                        {
                            PersonId = customer.Id,
                            Amount = _faker.Finance.Amount(),
                            StartDate = _faker.Date.Between(DateTime.Now.AddMonths(-12), DateTime.Now),
                            DueDate = _faker.Date.Between(DateTime.Now.AddMonths(2), DateTime.Now.AddMonths(12))
                        });
                }
            }

            context.Debts.AddRange(supplierDebts);
            context.SaveChanges();
        }

        private static void CreateSales(ApplicationDbContext context)
        {
            if (context.Sales.Any()) return;

            var customers = context.Customers.ToList();
            List<Sale> orders = new List<Sale>();
            var enumValues = Enum.GetValues(typeof(SaleType));

            foreach (var customer in customers)
            {
                int ordersCount = _random.Next(1, 25);

                for (int i = 0; i < ordersCount; i++)
                {
                    var totalDue = decimal.Round(_faker.Random.Decimal(10, 5000), 2);
                    var totalPaid = decimal.Round(_faker.Random.Decimal(0, totalDue), 2);
                    var discountPercentage = decimal.Round(_faker.Random.Decimal(0, 100), 2);
                    var discountTotal = decimal.Round((totalDue * discountPercentage) / 100, 2);
                    SaleType type = (SaleType)enumValues.GetValue(_random.Next(enumValues.Length));
                    var saleDate = _faker.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1));

                    orders.Add(
                        new Sale()
                        {
                            Receipt = _faker.Lorem.Word(),
                            SaleType = type,
                            SaleDate = saleDate,
                            TotalDue = totalDue,
                            TotalPaid = totalPaid,
                            DiscountPercentage = discountPercentage,
                            DiscountTotal = discountTotal,
                            Comment = _faker.Lorem.Sentence(null, 50),
                            CustomerId = customer.Id
                        });
                }
            }

            context.Sales.AddRange(orders);
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
                var orderItemsCount = _random.Next(1, 30);

                for (int i = 0; i < orderItemsCount; i++)
                {
                    orderItems.Add(
                        new SaleDetail()
                        {
                            Quantity = _random.Next(1, 20),
                            UnitPrice = decimal.Round(_faker.Random.Decimal(5, 500), 2),
                            UnitPriceDiscount = decimal.Round(_faker.Random.Decimal(0, 100), 2),
                            SaleId = sale.Id,
                            ProductId = products[_random.Next(0, products.Count)]?.Id ?? 1
                        });
                }
            }

            context.SaleDetails.AddRange(orderItems);
            context.SaveChanges();
        }

        private static void CreateSuppliers(ApplicationDbContext context)
        {
            if (context.Suppliers.Any()) return;

            List<Supplier> suppliers = new List<Supplier>();

            for (int i = 0; i < 500; i++)
            {
                suppliers.Add(
                    new Supplier()
                    {
                        FullName = _faker.Person.FullName,
                        CompanyName = _faker.Company.CompanyName(),
                        Balance = _faker.Random.Decimal(0, 10000),
                        IsActive = true
                    });
            }

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }

        private static void CreateSupplierAddresses(ApplicationDbContext context)
        {
            var suppliers = context.Suppliers.ToList();
            List<Address> supplierAddresses = new();

            foreach (var supplier in suppliers)
            {
                int numberOfPhones = _random.Next(0, 5);

                for (int i = 0; i < numberOfPhones; i++)
                {
                    supplierAddresses.Add(
                        new Address()
                        {
                            PersonId = supplier.Id,
                            AddressDetails = _faker.Address.FullAddress(),
                            AddressLandMark = _faker.Address.StreetAddress(),
                            Latitude = _faker.Address.Latitude(),
                            Longtitude = _faker.Address.Longitude(),
                        });
                }
            }

            context.Addresses.AddRange(supplierAddresses);
            context.SaveChanges();
        }

        private static void CreateSupplierPhones(ApplicationDbContext context)
        {
            var suppliers = context.Suppliers.ToList();
            List<Phone> supplierPhones = new();

            foreach (var supplier in suppliers)
            {
                int numberOfPhones = _random.Next(0, 5);

                for (int i = 0; i < numberOfPhones; i++)
                {
                    supplierPhones.Add(
                        new Phone()
                        {
                            PersonId = supplier.Id,
                            PhoneNumber = _faker.Phone.PhoneNumber()
                        });
                }
            }

            context.Phones.AddRange(supplierPhones);
            context.SaveChanges();
        }

        private static void CreateSupplierDebts(ApplicationDbContext context)
        {
            var suppliers = context.Suppliers.ToList();
            List<Debt> supplierDebts = new();

            foreach (var supplier in suppliers)
            {
                int numberOfDebts = _random.Next(0, 10);

                for (int i = 0; i < numberOfDebts; i++)
                {
                    supplierDebts.Add(
                        new Debt()
                        {
                            PersonId = supplier.Id,
                            Amount = _faker.Finance.Amount(),
                            StartDate = _faker.Date.Between(DateTime.Now.AddMonths(-12), DateTime.Now),
                            DueDate = _faker.Date.Between(DateTime.Now.AddMonths(2), DateTime.Now.AddMonths(12))
                        });
                }
            }

            context.Debts.AddRange(supplierDebts);
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

            foreach (var supply in supplies)
            {
                int suppliesCount = _random.Next(1, 25);

                for (int i = 0; i < suppliesCount; i++)
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

        private static void CreateInventories(ApplicationDbContext context)
        {
            if (context.Inventories.Any()) return;

            List<Inventory> inventories = new List<Inventory>();

            for(int i = 0; i < 5; i++)
            {
                inventories.Add(new Inventory(_faker.Lorem.Word()));
            }

            context.Inventories.AddRange(inventories);
            context.SaveChanges();
        }

        private static void CreateInventoryDetails(ApplicationDbContext context)
        {
            if (context.InventoryDetails.Any()) return;

            var inventories = context.Inventories.ToList();
            var products = context.Products.ToList();
            List<InventoryDetail> inventoryDetails = new List<InventoryDetail>();

            foreach(var inventory in inventories)
            {
                var randomProducts = GetRandomProducts(products);

                foreach(var product in randomProducts)
                {
                    inventoryDetails.Add(new InventoryDetail()
                    {
                        InventoryId = inventory.Id,
                        ProductId = product.Id,
                        ProductsRemained = _faker.Random.Double(0, 5000),
                        EnoughForDays = _faker.Random.Int(0, 30)
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
