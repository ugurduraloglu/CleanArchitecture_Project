using CleanArchitecture.Domain.Employees;
using CleanArchitecture_2025.Infrastructure.Context;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture_2025.Infrastructure.Repositories
{
    internal sealed class EmployeeRepository : Repository<Employee,ApplicationDbContext>, IEmployeeRepository     
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
