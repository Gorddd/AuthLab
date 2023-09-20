namespace AuthLab.Identity.DataAccess;

public class DatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public bool ApplyMigration {  get; set; }
}
