using Microsoft.EntityFrameworkCore;
using RentElectroScooter.CoreModels.Models;

namespace RentElectroScooter.DAL.Repositories
{
    public enum AvailableDatabases
    {
        MSSQL,
    }

    public class RentElectroscooterDBContext : DbContext
    {
        public virtual DbSet<ElectroScooter> ElectroScooters { get; set; }
        public virtual DbSet<SpecialProposition> SpecialPropositions { get; set; }
        public virtual DbSet<SpecialPropositionMetadata> SpecialPropositionMetadatas { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<SubscriptionMetadata> SubscriptionMetadatas { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<VehicleData> VehicleDatas { get; set; }

        public AvailableDatabases UsingDatabase { get; init; }

        public RentElectroscooterDBContext(DbContextOptions options, AvailableDatabases usingDatabase)
            : base(options) 
        { 
            UsingDatabase = usingDatabase;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RentElectroscooterDBContext).Assembly);
        }
    }
}
