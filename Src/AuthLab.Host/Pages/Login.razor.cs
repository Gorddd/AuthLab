using AuthLab.Domain.Entities;
using AuthLab.Domain.Responses;
using AuthLab.Host.Models;
using Microsoft.AspNetCore.Components;

namespace AuthLab.Host.Pages;

public partial class Login
{
    User User { get; set; } = new();

    DefenderResponse? DefenderResponse { get; set; }

    [Inject] Defender _defender { get; set; } = null!;

    void Submit()
    {

    }
}
