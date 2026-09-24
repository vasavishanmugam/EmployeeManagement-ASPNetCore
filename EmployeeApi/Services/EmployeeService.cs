using EmployeeManagement.Api.Models;
namespace EmployeeManagement.Api.Services;

public class EmployeeService
{
    private readonly List<Employee> _employees = 
    [
        new Employee {Id = 1,  Name = "Arun"},
        new Employee {Id = 2, Name = "Priya"}
    ];

    public List<Employee> GetEmployees()
    {
        return _employees;
    }

    public Employee? GetEmployee(int id)
    {
        return _employees.FirstOrDefault(e => e.Id == id);
    }
}