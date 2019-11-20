using Microsoft.EntityFrameworkCore;

namespace RandomApi.Models
{
    public class RandomContext : DbContext
    {
        public RandomContext(DbContextOptions<RandomContext> options)
            : base(options)
        {
        }

        public DbSet<DataPoint> DataPoints { get; set; }
    }
}