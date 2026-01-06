using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Employees;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture_2025.Infrastructure.Context
{
    internal sealed class ApplicationDbContext : DbContext , IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {
            
        }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<Entity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(p => p.CreateAt)
                        .CurrentValue = DateTimeOffset.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property(p => p.UpdateAt)
                        .CurrentValue = DateTimeOffset.Now;
                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
