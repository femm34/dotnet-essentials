using System.ComponentModel.DataAnnotations;
using MiApi.Utils;

namespace MiApi.Models.DTOs;

public class CreateUserDto
{
    [Required]
    [StringLength(40)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [PasswordValidation]
    public string Password { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string FirstLastName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string SecondLastName { get; set; } = string.Empty;
}