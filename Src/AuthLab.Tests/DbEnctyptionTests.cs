using AuthLab.DbEncryption.Concrete;

namespace AuthLab.Tests;

public class DbEnctyptionTests
{
    [Fact]
    public void EncryptAndDecrypt()
    {
        // Arrange
        var encryptedDbPath = "encrypted.txt";
        var tempDbPath = "tempdb.txt";
        var password = "123";
        var content = File.ReadAllText(tempDbPath);
        var encoder = new DbEncoder(encryptedDbPath, tempDbPath);

        // Act
        encoder.EncryptDatabase(password);
        
        // Assert
        var encryptedContent = File.ReadAllText(encryptedDbPath);
        Assert.NotEqual(content, encryptedContent);
        Assert.False(File.Exists(tempDbPath));
        Assert.True(File.Exists(encryptedDbPath));

        // Act
        encoder.DecryptDatabase(password);

        // Assert
        var decryptedContent = File.ReadAllText(tempDbPath);
        Assert.Equal(content, decryptedContent);
        Assert.False(File.Exists(encryptedDbPath));
        Assert.True(File.Exists(tempDbPath));
    }

    [Fact]
    public void Encrypt_DecryptWithWrongPassword()
    {
        // Arrange
        var encryptedDbPath = "encrypted.txt";
        var tempDbPath = "tempdb.txt";
        var password = "123";
        var encoder = new DbEncoder(encryptedDbPath, tempDbPath);

        // Act 
        encoder.EncryptDatabase(password);

        // Act
        var decryptResult = encoder.DecryptDatabase("WrongPassword");

        // Assert
        Assert.True(File.Exists(encryptedDbPath));
        Assert.False(File.Exists(tempDbPath));
        Assert.False(decryptResult);
    }
}
