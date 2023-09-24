using AuthLab.Domain.Entities;
using AuthLab.Domain.Responses;
using AuthLab.Host.Authentication;
using AuthLab.Host.Models;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AuthLab.Host.Pages;

public partial class Login
{
    User User { get; set; } = new();

    DefenderResponse? DefenderResponse { get; set; }

    [Inject] Defender _defender { get; set; } = null!;

    [Inject] IMapper _mapper { get; set; } = null!;

    [Inject] NavigationManager _navigation { get; set; } = null!;

    [Inject] AuthenticationStateProvider _authProvider { get; set; } = null!;

    async Task Submit()
    {
        DefenderResponse = await _defender.VerifyUser(_mapper.Map<UserValidationInformation>(User));
        if (!DefenderResponse.IsSuccess)
            return;

        var userInfo = DefenderResponse.UserInformation!;
        var customAuthProvider = (CustomAuthenticationStateProvider)_authProvider;
        await customAuthProvider.UpdateAuthenticationState(new UserSession
        {
            UserName = userInfo.Username,
            Role = userInfo.Role
        });

        _navigation.NavigateTo("/", true);
    }
}
