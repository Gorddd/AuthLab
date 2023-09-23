namespace AuthLab.Host.Application;

public record AppSettings
{
    public DatabaseSettings DatabaseSettings { get; init; } = null!;
}
