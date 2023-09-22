namespace AuthLab.Identity.DataAccess;

public class DatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public bool SeedData {  get; set; }
}
