namespace EmployeeManagement.Api.Models;

public class Project
{
    public int ProjectId { get; set;}
    public string  ProjectName {get; set;} = string.Empty;
    public ICollection<Employee> Employees {get; set;} = new List<Employee>();
}