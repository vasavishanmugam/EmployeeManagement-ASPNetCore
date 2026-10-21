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

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetEmployeeAsync(int id)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Employee> AddEmployeeAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return employee;
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