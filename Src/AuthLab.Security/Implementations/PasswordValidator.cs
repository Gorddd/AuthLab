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
        //Содержит цифру и знак препинания
        var punctuationMarks = new[]
        {
            '.', ',', ':', ';', '?', '\'', '-', '(', ')', '/', '\"', '!'
        };

        var result = password.Any(char.IsNumber) && password.Any(c => punctuationMarks.Any(p => p == c));
        return result;
    }
}
