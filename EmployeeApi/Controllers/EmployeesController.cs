using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Api.Services;
using EmployeeManagement.Api.Models;
using EmployeeManagement.Api.DTOs;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController: ControllerBase
{
    private readonly IEmployeeService _employeeService;
    
    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees(
    [FromQuery] string? name,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string? sortBy = null)
    {
        if (pageNumber < 1 || pageSize < 1 || pageSize > 100)
        {
            return BadRequest("Invalid pagination value.");
        }

        var result = await _employeeService.GetEmployeesAsync(
            name, pageNumber, pageSize, sortBy);

        var employees = result.Employees.Select(e => new EmployeeResponseDto
        {
            Id = e.Id,
            Name = e.Name,
            DepartmentId = e.DepartmentId,

            Department = e.Department == null ? null : new DepartmentSummaryDto
            {
                DepartmentId = e.Department.DepartmentId,
                DepartmentName = e.Department.DepartmentName
            },

            Projects = e.Projects.Select(p => new ProjectSummaryDto
            {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName
            }).ToList()
        }).ToList();

        return Ok(new
        {
            totalCount = result.TotalCount,
            pageNumber,
            pageSize,
            totalPages = (int)Math.Ceiling(
                (double)result.TotalCount / pageSize),
            employees
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var employee = await _employeeService.GetEmployeeAsync(id);

        if (employee is null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployee(EmployeeCreateDto employeeDto)
    {
        var employee = new Employee
        {
            Name = employeeDto.Name
        };

        var createdEmployee = await _employeeService.AddEmployeeAsync(employee);

        var response = new EmployeeResponseDto
        {
            Id = createdEmployee.Id,
            Name = createdEmployee.Name,
            DepartmentId = createdEmployee.DepartmentId,

            Department = createdEmployee.Department == null ? null : new DepartmentSummaryDto
            {
                DepartmentId = createdEmployee.Department.DepartmentId,
                DepartmentName = createdEmployee.Department.DepartmentName
            },

            Projects = createdEmployee.Projects
                .Select(p => new ProjectSummaryDto
                {
                    ProjectId = p.ProjectId,
                    ProjectName = p.ProjectName
                })
                .ToList()
        };
        return CreatedAtAction(
            nameof(GetEmployee),
            new { id = createdEmployee.Id },
            response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, EmployeeUpdateDto employeeDto)
    {
        var employee = new Employee
        {
            Name = employeeDto.Name
        };

        var updatedEmployee  = await _employeeService.UpdateEmployeeAsync(id, employee);
        
        if (updatedEmployee is null)
        {
            return NotFound();
        }

        return Ok(updatedEmployee);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var DeleteEmployee = await _employeeService.DeleteEmployeeAsync(id);
        if (!DeleteEmployee)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchEmployees([FromQuery] string name)
    {
        var employees = await _employeeService.GetEmployeesByNameAsync(name);
        return Ok(employees);
    }

    [HttpGet("sorted")]
    public async Task<IActionResult> GetEmployeesSorted()
    {
        var employees = await _employeeService.GetEmployeesSortedByNameAsync();
        return Ok(employees);
    }

    [HttpGet("names")]
    public async Task<IActionResult> GetEmmployeeNames()
    {
        var names = await _employeeService.GetEmployeeNamesAsync();
        return Ok(names);
    }
}