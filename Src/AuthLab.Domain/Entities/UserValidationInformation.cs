namespace AuthLab.Domain.Entities;

public record UserValidationInformation
{
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
}
