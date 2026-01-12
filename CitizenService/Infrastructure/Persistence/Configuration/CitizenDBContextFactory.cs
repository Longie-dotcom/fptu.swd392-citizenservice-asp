using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence.Configuration
{
    public class CitizenDbContextFactory : IDesignTimeDbContextFactory<CitizenDBContext>
    {
        public CitizenDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CitizenDBContext>();

            optionsBuilder.UseSqlServer(
                "Server=.;Database=CitizenDB;Trusted_Connection=True;TrustServerCertificate=True");

            return new CitizenDBContext(optionsBuilder.Options);
        }
    }
}