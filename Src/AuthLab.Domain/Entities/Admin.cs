using AuthLab.Domain.Abstractions;
using AuthLab.Domain.Responses;

namespace AuthLab.Domain.Entities;

public class Admin
{
    private readonly IUsers _users;

    public Admin(IUsers users)
    {
        _users = users;
    }

    public async Task<AdminResponse> ChangeUserBlocking(string username, bool setValue)
    {
        if (await _users.IsThereUser(username))
        {
            var user = await _users.GetUserByName(username);
            user.IsBlocked = setValue;
            await _users.UpdateUser(user);

            return new AdminResponse { IsSuccess = true };
        }
        return new AdminResponse { IsSuccess = false, Message = $"Пользователь {username} не найден" };
    }

    public async Task<AdminResponse> ChangeUserPasswordRequirements(string username, bool setValue)
    {
        if (await _users.IsThereUser(username))
        {
            var user = await _users.GetUserByName(username);
            user.HasPasswordRequirements = setValue;
            await _users.UpdateUser(user);
        }
        return new AdminResponse { IsSuccess = false, Message = $"Пользователь {username} не найден" };
    }
}
