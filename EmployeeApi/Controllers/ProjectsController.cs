using EmployeeManagement.Api.Data;
using EmployeeManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Api.DTOs;

namespace EmployeeManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController: ControllerBase
{
    private readonly EmployeeDbContext _context;

    public ProjectsController(EmployeeDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProject), new {id = project.ProjectId}, project);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        var project = await _context.Projects
            .Include(p => p.Employees)
            .Where(p => p.ProjectId == id)
            .Select(p => new ProjectDetailsDto
            {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName,
                Employees = p.Employees.Select(e => new EmployeeSummaryDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    DepartmentId = e.DepartmentId
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (project == null)
            return NotFound();

        return Ok(project);
    }

        [HttpPost("{projectId}/employees/{employeeId}")]
    public async Task<IActionResult> AssignEmployee(
        int projectId, int employeeId)
    {
        var project = await _context.Projects
            .Include(p => p.Employees)
            .FirstOrDefaultAsync(p => p.ProjectId == projectId);

        if (project == null)
            return NotFound("Project not found.");

        var employee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == employeeId);

        if (employee == null)
            return NotFound("Employee not found.");

        if (project.Employees.Any(e => e.Id == employeeId))
            return BadRequest("Employee already assigned to this project.");

        project.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return Ok("Employee assigned to project successfully.");
    }
}