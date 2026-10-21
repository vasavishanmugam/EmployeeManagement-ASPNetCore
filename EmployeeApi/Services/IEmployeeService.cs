using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetEmployeesAsync();
    Task<Employee?> GetEmployeeAsync(int id);
    Task<Employee> AddEmployeeAsync(Employee employee);
    Task<Employee?> UpdateEmployeeAsync(int id, Employee employee);
    Task<bool> DeleteEmployeeAsync(int id);
    Task<List<Employee>> GetEmployeesByNameAsync(string name);
    Task<List<Employee>> GetEmployeesSortedByNameAsync();
    Task<List<string>> GetEmployeeNamesAsync();
}