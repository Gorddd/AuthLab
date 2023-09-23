using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLab.Domain.Abstractions;

public interface ISecurityService
{
    public string HashPassword(string password);

    public bool VerifyPassword(string password, string hash);
}
