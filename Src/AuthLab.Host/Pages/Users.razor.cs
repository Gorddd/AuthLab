using AuthLab.Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace AuthLab.Host.Pages;

public partial class Users
{
    private IEnumerable<UserInformation> _users = null!;

    [Inject] private Admin AdminService { get; set; } = null!;

    protected async override Task OnInitializedAsync()
    {
        _users = await AdminService.GetUsers();
    }
}
