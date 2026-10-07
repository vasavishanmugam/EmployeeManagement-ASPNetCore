using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Api.Services;
using EmployeeManagement.Api.Models;
using EmployeeManagement.Api.DTOs;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController: ControllerBase
{
    private readonly IEmployeeService _employeesService;
    
    public EmployeesController(IEmployeeService employeesService)
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

    [HttpPost]
    public IActionResult AddEmployee(EmployeeCreateDto employeeDto)
    {
        var employee = new Employee
        {
            Name = employeeDto.Name
        };

        var createdEmployee = _employeesService.AddEmployee(employee);
        
        return CreatedAtAction(
            nameof(GetEmployee),
            new {id = createdEmployee.Id},
            createdEmployee);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEmployee(int id, EmployeeUpdateDto employeeDto)
    {
        var employee = new Employee
        {
            Name = employeeDto.Name
        };

        var updatedEmployee  = _employeesService.UpdateEmployee(id, employee);
        if (updatedEmployee == null)
        {
            return NotFound();
        }

        return Ok(updatedEmployee);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        var DeleteEmployee = _employeesService.DeleteEmployee(id);
        if (!DeleteEmployee)
        {
            return NotFound();
        }

        return NoContent();
    }
}