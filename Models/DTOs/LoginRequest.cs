using System.ComponentModel.DataAnnotations;
using MiApi.Utils;

namespace MiApi.Models.DTOs;

public class LoginRequest
{
    [Required]
    [PasswordValidation]
    public string Password { get; set; } = string.Empty;
 
    [Required]
    public string Username { get; set; } = string.Empty;
}