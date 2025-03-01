using Microsoft.EntityFrameworkCore;
using RestaurantChainManagement.Models;


namespace RestaurantChainManagement.Data
{
    public class ApplicationDbContext : DbContext
    {

        // Constructor that accepts options which will be configured in Startup/Program.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // DbSets for Locations domain
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }

    }
}
