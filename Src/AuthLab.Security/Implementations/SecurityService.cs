using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthLab.Domain.Abstractions;

namespace AuthLab.Security.Implementations;

public class SecurityService : ISecurityService
{
    public string HashPassword(string password)
    {
        return SecurePasswordHasher.Hash(password);
    }

    public bool VerifyPassword(string password, string hash)
    {
        return SecurePasswordHasher.Verify(password, hash);
    }
}
