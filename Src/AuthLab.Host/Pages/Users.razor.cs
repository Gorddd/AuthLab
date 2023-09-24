using AuthLab.Domain.Entities;
using AuthLab.Domain.Responses;
using Microsoft.AspNetCore.Components;

namespace AuthLab.Host.Pages;

public partial class Users
{
    private IEnumerable<UserInformation> _users = null!;

    [Inject] private Admin AdminService { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    private AdminResponse AdminResponse { get; set; } = null!;

    protected async override Task OnInitializedAsync()
    {
        _users = await AdminService.GetUsers();
    }

    private async Task SetBlockingValue(string username, bool value)
    {
        AdminResponse = await AdminService.ChangeUserBlocking(username, value);

        if (AdminResponse.IsSuccess)
            NavigationManager.NavigateTo("/users", true);
    }

    private async Task SetPasswordRequirements(string username, bool value)
    {
        AdminResponse = await AdminService.ChangeUserPasswordRequirements(username, value);

        if (AdminResponse.IsSuccess)
            NavigationManager.NavigateTo("/users", true);
    }

    private async Task DeleteUser(string username)
    {
        AdminResponse = await AdminService.DeleteUser(new UserValidationInformation
        {
            Username = username
        });

        if (AdminResponse.IsSuccess)
            NavigationManager.NavigateTo("/users", true);
    }
}
