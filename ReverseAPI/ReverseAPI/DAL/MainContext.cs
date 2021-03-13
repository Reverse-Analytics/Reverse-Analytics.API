using Microsoft.EntityFrameworkCore;
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
    }
}
