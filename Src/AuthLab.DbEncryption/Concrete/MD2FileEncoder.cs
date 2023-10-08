using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthLab.DbEncryption.Concrete;

static class MD2FileEncoder
{
    public static void EncryptFile(string inputFile, string outputFile, string password)
    {
        using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
        using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
        {
            byte[] key = GetMD2Hash(password);
            byte[] iv = GenerateRandomIV();

            using (AesManaged aes = new AesManaged())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CFB;

                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                using (CryptoStream cryptoStream = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                {
                    fsOutput.Write(iv, 0, iv.Length);
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cryptoStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        File.Delete(inputFile);
    }

    public static void DecryptFile(string inputFile, string outputFile, string password)
    {
        using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
        using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
        {
            byte[] key = GetMD2Hash(password);
            byte[] iv = new byte[16]; // Read the IV from the file

            fsInput.Read(iv, 0, 16);

            using (AesManaged aes = new AesManaged())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CFB;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                using (CryptoStream cryptoStream = new CryptoStream(fsOutput, decryptor, CryptoStreamMode.Write))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cryptoStream.Write(buffer, 0, bytesRead);
                    }
                }
           }
        }

        File.Delete(inputFile);
    }

    static byte[] GetMD2Hash(string input)
    {
        var md2 = new MD2();
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        return md2.ComputeHash(inputBytes);
    }

    static byte[] GenerateRandomIV()
    {
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            byte[] iv = new byte[16];
            rng.GetBytes(iv);
            return iv;
        }
    }
}
