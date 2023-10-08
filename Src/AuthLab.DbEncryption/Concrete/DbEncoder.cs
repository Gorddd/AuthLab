using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLab.DbEncryption.Concrete;

public class DbEncoder
{
    private string EncryptedDbPath {  get; set; }
    private string TempDbPath {  get; set; }

    public DbEncoder(string encryptedDbPath, string tempDbPath)
    {
        EncryptedDbPath = encryptedDbPath;
        TempDbPath = tempDbPath;
    }

    public bool DecryptDatabase(string password)
    {
        try
        {
            MD2FileEncoder.DecryptFile(EncryptedDbPath, TempDbPath, password);
        }
        catch
        {
            File.Delete(TempDbPath);
            return false;
        }
        return true;
    }

    public bool EncryptDatabase(string password)
    {
        if (string.IsNullOrEmpty(password))
            return false;

        MD2FileEncoder.EncryptFile(TempDbPath, EncryptedDbPath, password);
        return true;
    }
}
