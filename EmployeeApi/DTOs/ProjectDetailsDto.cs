namespace EmployeeManagement.Api.DTOs;

public class ProjectDetailsDto
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public List<EmployeeSummaryDto> Employees { get; set; } = new();
}

public class EmployeeSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
}