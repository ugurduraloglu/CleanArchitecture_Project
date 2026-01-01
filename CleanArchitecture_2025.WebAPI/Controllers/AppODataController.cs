using CleanArchitecture_2025.Application.Employees;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace CleanArchitecture_2025.WebAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("odata")]
    public class AppODataController(ISender sender) : ODataController
    {
        public async Task<IActionResult> GetAllEmployees(CancellationToken cancellationToken)
        {
            var response = await sender.Send(new EmployeeGetAllQuery(), cancellationToken);
            return Ok(response); 
        }
    }
}
