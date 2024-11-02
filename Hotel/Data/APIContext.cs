using Microsoft.EntityFrameworkCore;

namespace Hotel.Data
{
    public class APIContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {

        }
    }
}
