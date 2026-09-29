using EmployeeManagement.Api.Models;
namespace EmployeeManagement.Api.Services;

public class EmployeeService : IEmployeeService
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

    public Employee AddEmployee(Employee employee)
    {
        employee.Id = _employees.Count == 0
        ? 1 : _employees.Max(e => e.Id) + 1;
        _employees.Add(employee);

        return employee;
    }

    public Employee? UpdateEmployee(int id, Employee employee)
    {
        var existingEmployee = _employees.FirstOrDefault(e => e.Id == id);
        if (existingEmployee == null)
        {
            return null;
        }

        existingEmployee.Name = employee.Name;

        return existingEmployee;
    }

    public bool DeleteEmployee(int id)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
        {
            return false;
        }

        _employees.Remove(employee);
        return true;
    }
}