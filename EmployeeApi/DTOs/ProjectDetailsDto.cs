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

public class ProjectSummaryDto
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
}

public class EmployeeResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public DepartmentSummaryDto? Department { get; set; }
    public List<ProjectSummaryDto> Projects { get; set; } = new();
}

public class DepartmentSummaryDto
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
}