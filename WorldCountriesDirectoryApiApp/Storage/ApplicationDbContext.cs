using Microsoft.EntityFrameworkCore;

namespace WorldCountriesDirectoryApiApp.Storage
{
    public class ApplicationDbContext : DbContext
    {
        public  DbSet<DbCountry> Countries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string useConnection = configuration.GetSection("UseConnection").Value ?? "DefailtConnection";
            string? connectionString = configuration.GetConnectionString(useConnection);
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
