﻿using Microsoft.EntityFrameworkCore;
using ReverseAPI.Models;

namespace ReverseAPI.DAL
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Supply> Supplies { get; set; }
    }
}
