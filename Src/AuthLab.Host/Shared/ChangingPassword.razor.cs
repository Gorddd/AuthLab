using AuthLab.Domain.Entities;
using AuthLab.Domain.Responses;
using AuthLab.Host.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace AuthLab.Host.Shared;

public partial class ChangingPassword
{
    ChangePasswordModel PasswordModel { get; set; } = new();

    DefenderResponse? DefenderResponse { get; set; }

    [Inject] Defender Defender { get; set; } = null!;

    [Inject] AuthenticationStateProvider authProvider { get; set; } = null!;
    [Inject] NavigationManager NavigationManager { get; set; } = null!;

    async Task Sumbit()
    {
        if (PasswordModel.NewPasswordFirst != PasswordModel.NewPasswordSecond)
        {
            DefenderResponse = new DefenderResponse { IsSuccess = false, Message = "Новый пароль не совпадает с повторенным новым паролем" };
            return;
        }

        var authState = await authProvider.GetAuthenticationStateAsync();

        DefenderResponse = await Defender.ChangePassword(new UserValidationInformation
        {
            Username = authState.User.Identity!.Name!,
            Password = PasswordModel.CurrentPassword
        },
            PasswordModel.NewPasswordFirst);

        if (DefenderResponse.IsSuccess)
            NavigationManager.NavigateTo("/", true);
    }
}
