using System.ComponentModel.DataAnnotations;

namespace AuthLab.Host.Models;

public class User
{
    [Required]
    public string? Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
