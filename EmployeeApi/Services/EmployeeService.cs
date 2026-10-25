using EmployeeManagement.Api.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Api.Data;

namespace EmployeeManagement.Api.Services;

public class EmployeeService : IEmployeeService
{
    private readonly EmployeeDbContext _context;

    public EmployeeService(EmployeeDbContext context)
    {
        _context = context;
    }

public async Task<(List<Employee> Employees, int TotalCount)> GetEmployeesAsync(
    string? name,
    int pageNumber,
    int pageSize,
    string? sortBy)
{
    var query = _context.Employees
        .Include(e => e.Department)
        .Include(e => e.Projects)
        .AsQueryable();

    // Filter by employee name
    if (!string.IsNullOrWhiteSpace(name))
    {
        query = query.Where(e => e.Name.Contains(name));
    }

    // Sort employees
    query = sortBy?.ToLower() == "name"
        ? query.OrderBy(e => e.Name)
        : query.OrderBy(e => e.Id);

    // Get total count before pagination
    var totalCount = await query.CountAsync();

    // Apply pagination
    var employees = await query
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    return (employees, totalCount);
}

    public async Task<Employee?> GetEmployeeAsync(int id)
    {
        return await _context.Employees.Include(e=>e.Department).FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Employee> AddEmployeeAsync(Employee employee)
    {
        var departmentIds = await _context.Departments
            .Select(d => d.DepartmentId)
            .ToListAsync();

        if (departmentIds.Count == 0)
            throw new InvalidOperationException("No departments available.");

        var projects = await _context.Projects.ToListAsync();

        if (projects.Count == 0)
            throw new InvalidOperationException("No projects available.");

        var random = new Random();

        employee.DepartmentId = departmentIds[random.Next(departmentIds.Count)];

        var selectedProject = projects[random.Next(projects.Count)];
        employee.Projects.Add(selectedProject);

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Projects)
            .FirstAsync(e => e.Id == employee.Id);
    }

    public async Task<Employee?> UpdateEmployeeAsync(int id, Employee employee)
    {
        var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if (existingEmployee is null)
        {
            return null;
        }

        existingEmployee.Name = employee.Name;

        await _context.SaveChangesAsync();
        return existingEmployee;
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
       
        if (employee is null)
        {
            return false;
        }

       _context.Employees.Remove(employee);
       await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Employee>> GetEmployeesByNameAsync(string name)
    {
        return await _context.Employees.Where(e => e.Name.Contains(name)).ToListAsync();
    }

    public async Task<List<Employee>> GetEmployeesSortedByNameAsync()
    {
        return await _context.Employees.OrderBy(e => e.Name).ToListAsync();
    }

    public async Task<List<String>> GetEmployeeNamesAsync()
    {
        return await _context.Employees.Select(e => e.Name).ToListAsync();
    }
}