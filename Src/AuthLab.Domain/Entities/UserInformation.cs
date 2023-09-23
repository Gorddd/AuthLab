using System;
namespace AuthLab.Domain.Entities;

public record UserInformation
{
    public string Username { get; init; } = null!;
    public string HashedPassword { get; set; } = null!;
    public bool HasPasswordRequirements { get; set; }
    public bool IsBlocked { get; set; }
    public string Role { get; init; } = null!;
}
