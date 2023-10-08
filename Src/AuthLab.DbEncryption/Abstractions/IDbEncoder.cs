namespace AuthLab.DbEncryption.Abstractions;

public interface IDbEncoder
{
    public bool DecryptDatabase(string password);

    public bool EncryptDatabase(string password);
}