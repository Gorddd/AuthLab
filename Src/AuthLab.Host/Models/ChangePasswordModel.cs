using System.ComponentModel.DataAnnotations;

namespace AuthLab.Host.Models;

public class ChangePasswordModel
{
    [Required]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required]
    public string NewPasswordFirst { get; set; } = string.Empty;

    [Required]
    public string NewPasswordSecond { get; set; } = string.Empty;
}
