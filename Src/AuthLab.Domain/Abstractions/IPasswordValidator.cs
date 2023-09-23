using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthLab.Domain.Responses;

namespace AuthLab.Domain.Abstractions;

public interface IPasswordValidator
{
    public bool ValidatePassword(string password);
}
