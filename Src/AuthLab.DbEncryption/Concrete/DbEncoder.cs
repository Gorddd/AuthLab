using AuthLab.DbEncryption.Abstractions;

namespace AuthLab.DbEncryption.Concrete;

public class DbEncoder : IDbEncoder
{
    private string _encryptedDbPath { get; set; }
    private string _tempDbPath { get; set; }

    public DbEncoder(string encryptedDbPath, string tempDbPath)
    {
        _encryptedDbPath = encryptedDbPath;
        _tempDbPath = tempDbPath;
    }

    public bool DecryptDatabase(string password)
    {
        try
        {
            MD2FileEncoder.DecryptFile(_encryptedDbPath, _tempDbPath, password);
        }
        catch
        {
            File.Delete(_tempDbPath);
            return false;
        }
        return true;
    }

    public bool EncryptDatabase(string password)
    {
        if (string.IsNullOrEmpty(password))
            return false;

        MD2FileEncoder.EncryptFile(_tempDbPath, _encryptedDbPath, password);
        return true;
    }
}
