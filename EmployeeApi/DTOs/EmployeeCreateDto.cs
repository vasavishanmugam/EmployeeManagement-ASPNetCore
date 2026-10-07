using System.ComponentModel.DataAnnotations;
namespace EmployeeManagement.Api.DTOs;

public class EmployeeCreateDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name {get; set; } = string.Empty;
}