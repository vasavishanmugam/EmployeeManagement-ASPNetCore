using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController: ControllerBase
{
    [HttpGet]
    public IActionResult GetEmployees()
    {
        return Ok(new []
        {
            new {Id = 1, Name = "Arun"},
            new {Id = 2, Name = "Priya"}
        });
    }
    [HttpGet("{id}")]
    public IActionResult GetEmployee(int id)
    {
        var employees = new []
        {
            new {Id = 1, Name = "Arun"},
            new {Id = 2, Name = "Priya"}
        };

        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
        return NotFound();

        return Ok(employee);
    }
}