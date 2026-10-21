using System.ComponentModel.DataAnnotations;
namespace EmployeeManagement.Api.DTOs;

public class EmployeeCreateDto
{
    [Required(ErrorMessage = "Employee name is required.")]
    [StringLength(100, MinimumLength = 2,
    ErrorMessage =  "Name must be between 2 and 100 characters.")]
    public string Name {get; set; } = string.Empty;
}