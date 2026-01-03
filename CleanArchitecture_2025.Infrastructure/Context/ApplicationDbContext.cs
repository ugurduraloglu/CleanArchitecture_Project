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

    }
}
