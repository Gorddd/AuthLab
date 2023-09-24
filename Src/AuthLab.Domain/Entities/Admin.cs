using AuthLab.Domain.Abstractions;
using AuthLab.Domain.Responses;

namespace AuthLab.Domain.Entities;

public class Admin
{
    private readonly IUsers _users;
    private readonly ISecurityService _securityService;

    public Admin(IUsers users, ISecurityService serService)
    {
        _users = users;
        _securityService = serService;
    }

    public async Task<AdminResponse> ChangeUserBlocking(string username, bool setValue)
    {
        if (!await _users.IsThereUser(username))
            return new AdminResponse { IsSuccess = false, Message = $"Пользователь {username} не найден" };

        var user = await _users.GetUserByName(username);
        user.IsBlocked = setValue;
        await _users.UpdateUser(user);

        return new AdminResponse { IsSuccess = true };

    }

    public async Task<AdminResponse> ChangeUserPasswordRequirements(string username, bool setValue)
    {
        if (!await _users.IsThereUser(username))
            return new AdminResponse { IsSuccess = false, Message = $"Пользователь {username} не найден" };

        var user = await _users.GetUserByName(username);
        user.HasPasswordRequirements = setValue;
        await _users.UpdateUser(user);

        return new AdminResponse { IsSuccess = true };
    }

    public async Task<IEnumerable<UserInformation>> GetUsers()
    {
        return await _users.GetUsers();
    }

    public async Task<AdminResponse> DeleteUser(UserValidationInformation user)
    {
        if (!await _users.IsThereUser(user.Username))
            return new AdminResponse { IsSuccess = false, Message = $"Пользователь {user.Username} не найден" };

        await _users.DeleteUser(user.Username);
        return new AdminResponse { IsSuccess = true };
    }

    public async Task<AdminResponse> AddNewUser(UserValidationInformation user)
    {
        if (await _users.IsThereUser(user.Username))
            return new AdminResponse { IsSuccess = false, Message = $"Пользователь с именем {user.Username} уже существует!" };

        await _users.AddUser(new UserInformation
        {
            Username = user.Username,
            HashedPassword = _securityService.HashPassword(user.Password),
            Role = "User"
        });
        return new AdminResponse { IsSuccess = true };
    }
}
