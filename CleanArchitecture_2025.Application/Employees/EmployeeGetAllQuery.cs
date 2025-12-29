using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Employees;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture_2025.Application.Employees;

public sealed record EmployeeGetAllQuery() : IRequest<IQueryable<EmployeeGetAllQueryResponse>>;

public sealed class EmployeeGetAllQueryResponse : EntityDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateOnly BirtOfDate { get; set; }
    public decimal Salary { get; set; }
    public string TCNo { get; set; } = default!;
    public Address? Address { get; set; }
}

internal sealed class EmployeeGetAllQueryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<EmployeeGetAllQuery, IQueryable<EmployeeGetAllQueryResponse>>
{
    public Task<IQueryable<EmployeeGetAllQueryResponse>> Handle(EmployeeGetAllQuery request, CancellationToken cancellationToken)
    {
        var response = employeeRepository.GetAll()
            .Select(s => new EmployeeGetAllQueryResponse
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                Salary = s.Salary,
                BirtOfDate= s.BirtOfDate,
                CreateAt = s.CreateAt,
                DeleteAt = s.DeleteAt,
                Id = s.Id,
                IsDeleted = s.IsDeleted,
                TCNo = s.PersonalInformation.TCNo,
                UpdateAt = s.UpdateAt,
            }).AsQueryable();

        return Task.FromResult(response);
    }
}

