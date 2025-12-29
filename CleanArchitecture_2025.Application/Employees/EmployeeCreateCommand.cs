using CleanArchitecture.Domain.Employees;
using FluentValidation;
using GenericRepository;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace CleanArchitecture_2025.Application.Employees
{
    public sealed record EmployeeCreateCommand(
        string FirstName,
        string LastName,
        DateOnly BirthOfDate,
        decimal Salary,
        PersonalInformation PersonalInformation,
        Address? Address) : IRequest<Result<string>>;


    public sealed class EmployeeCreateCommandValidator : AbstractValidator<EmployeeCreateCommand>
    {
        public EmployeeCreateCommandValidator() 
        {
            RuleFor(x => x.FirstName).MinimumLength(3).WithMessage("Ad alanı en az 3 karakter olmalı!");
            RuleFor(x => x.LastName).MinimumLength(3).WithMessage("Soyad alanı en az 3 karakter olmalı!");
            RuleFor(x => x.PersonalInformation.TCNo)
                .MinimumLength(11).WithMessage("Geçerli bir TC no giriniz!!")
                .MaximumLength(11).WithMessage("Geçerli bir TC no giriniz!!");
        }
    }



    internal sealed class EmployeeCreateCommandHandler(IEmployeeRepository employeeRepository,IUnitOfWork unitOfWork) : IRequestHandler<EmployeeCreateCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
        {
            var isEmployeeExists = await employeeRepository.AnyAsync(p=> p.PersonalInformation.TCNo==request.PersonalInformation.TCNo,cancellationToken);

            if (isEmployeeExists)
            {
                return Result<string>.Failure("Bu TC numarası daha önce kaydedilmiş");
            }

            Employee employee = request.Adapt<Employee>();

            employeeRepository.Add(employee);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Personel kaydı başarıyla tamamlandı.";
        }
    }


}
