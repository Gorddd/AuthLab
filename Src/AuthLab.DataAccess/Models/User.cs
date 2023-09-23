using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLab.DataAccess.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; init; } = null!;
    public string HashedPassword { get; set; } = null!;
    public bool HasPasswordRequirements { get; set; }
    public bool IsBlocked { get; set; }
    public string Role { get; init; } = null!;
}
