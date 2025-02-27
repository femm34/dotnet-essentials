using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MiApi.Utils;

public class PasswordValidationAttribute : ValidationAttribute
{
    private static readonly Regex passwordRegex = new Regex(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?""{}|<>]).{8,}$", 
        RegexOptions.Compiled);

    public PasswordValidationAttribute() : base("La contraseña no cumple con los requisitos.") { }

    public override bool IsValid(object value)
    {
        if (value == null)
            return false;

        var password = value as string;
        return password != null && passwordRegex.IsMatch(password);
    }
}