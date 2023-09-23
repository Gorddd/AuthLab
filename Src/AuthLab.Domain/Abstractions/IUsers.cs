using AuthLab.Domain.Entities;

namespace AuthLab.Domain.Abstractions;

public interface IUsers
{
    public Task<UserInformation> GetUserByName(string username);

    public Task<bool> IsThereUser(string username);

    public Task UpdateUser(UserInformation userInformation);

    public Task<IEnumerable<UserInformation>> GetUsers();

    public Task AddUser(UserInformation newUsers);
}
