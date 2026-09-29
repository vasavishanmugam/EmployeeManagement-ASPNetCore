using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Services;

public interface IEmployeeService
{
    List<Employee> GetEmployees();
    Employee? GetEmployee(int id);
    Employee AddEmployee(Employee employee);
    Employee? UpdateEmployee(int id, Employee employee);
    bool DeleteEmployee(int id);
}