using System.Text.Json.Serialization;

namespace EmployeeManagement.Api.Models;

public class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = string.Empty;
    [JsonIgnore]
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}