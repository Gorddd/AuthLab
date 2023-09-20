using AuthLab.Identity.DataAccess;

namespace AuthLab.Identity.Application;

public class AppSettings
{
    public DatabaseSettings DatabaseSettings { get; set; } = null!;
}
