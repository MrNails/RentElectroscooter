using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RentElectroScooter.DAL.Repositories;

namespace RentElectroScooter.DAL.ContextFactories
{
    internal class RentElectroscooterDBCtxFactory : IDesignTimeDbContextFactory<RentElectroscooterDBContext>
    {
        public RentElectroscooterDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RentElectroscooterDBContext>();
            optionsBuilder.UseSqlServer("data source=DESKTOP-U6JULRK;initial catalog=RentElectroScooterDB;integrated security=true;Persist Security Info=true;MultipleActiveResultSets=True;TrustServerCertificate=true");

            return new RentElectroscooterDBContext(optionsBuilder.Options, AvailableDatabases.MSSQL);
        }
    }
}
