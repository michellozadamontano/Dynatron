using DynatronWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DynatronWebApi.Database
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        ///     Application Database Context
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        ///     Model creation for database configuration
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        ///     Database configuration
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        /// <summary>
        ///     Save changes with audit information
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastUpdated = DateTime.UtcNow;
                        break;
                }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}