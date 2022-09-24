﻿using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Customer>> FindAllCustomers(string? searchString, int pageNumber, int pageSize)
        {
            var customers = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.FirstName.Contains(searchString) ||
                                                 c.LastName.Contains(searchString) ||
                                                 (c.Address != null && c.Address.Contains(searchString)) ||
                                                 (c.CompanyName != null && c.CompanyName.Contains(searchString)));
            }

            customers = customers.OrderBy(s => s.FirstName)
                .ThenBy(x => x.LastName);

            customers = customers.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize);

            return await customers.ToListAsync();
        }
    }
}
