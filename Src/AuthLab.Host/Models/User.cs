using System.ComponentModel.DataAnnotations;

namespace AuthLab.Host.Models;

public class User
{
    [Required]
    public string? Username { get; init; }
    [Required]
    public string? Password { get; set; } = null!;
}
