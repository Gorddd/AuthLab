using AuthLab.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLab.Security.Implementations;

public class PasswordValidator : IPasswordValidator
{
    public bool ValidatePassword(string password)
    {
        return true;
    }
}
