using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Services;

public interface IEmployeeService
{
    List<Employee> GetEmployees();
    Employee? GetEmployee(int id);
}