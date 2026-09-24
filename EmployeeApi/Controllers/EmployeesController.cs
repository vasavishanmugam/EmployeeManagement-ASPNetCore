using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Api.Services;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController: ControllerBase
{
    private readonly EmployeeService _employeesService;
    
    public EmployeesController(EmployeeService employeesService)
    {
        _employeesService = employeesService;
    }

    [HttpGet]
    public IActionResult GetEmployees()
    {
        return Ok(_employeesService.GetEmployees());
    }

    [HttpGet("{id}")]
    public IActionResult GetEmployee(int id)
    {
        var employee = _employeesService.GetEmployee(id);

        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }
}