using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.TestDataCreator
{
    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext _context;

        public DatabaseSeeder(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
