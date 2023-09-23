namespace AuthLab.Host.Application;

public record DatabaseSettings
{
    public string ConnectionString { get; init; } = null!;
    public bool ApplyMigration { get; init; }
}
