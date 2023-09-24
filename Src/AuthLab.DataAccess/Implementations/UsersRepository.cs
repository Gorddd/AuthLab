using AuthLab.DataAccess.EfCore;
using AuthLab.Domain.Abstractions;
using AuthLab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AuthLab.DataAccess.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AuthLab.DataAccess.Implementations;

public class UsersRepository : IUsers
{
    private readonly UsersDbContext _context;
    private readonly IMapper _mapper;

    public UsersRepository(UsersDbContext usersDbContext, IMapper mapper)
    {
        _context = usersDbContext;
        _mapper = mapper;
    }

    public async Task AddUser(UserInformation newUser)
    {
        await _context.Users.AddAsync(_mapper.Map<UserInformation, User>(newUser));
        await _context.SaveChangesAsync();
    }

    public async Task<UserInformation> GetUserByName(string username)
    {
        return await _context.Users.ProjectTo<UserInformation>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .SingleAsync(u => u.Username == username);
    }

    public async Task<IEnumerable<UserInformation>> GetUsers()
    {
        return await _context.Users.ProjectTo<UserInformation>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> IsThereUser(string username)
    {
        return await _context.Users.ProjectTo<UserInformation>(_mapper.ConfigurationProvider)
            .AnyAsync(u => u.Username == username);
    }

    public async Task DeleteUser(string username)
    {
        var userToDelete = await _context.Users.SingleAsync(u => u.Username == username);
        _context.Users.Remove(userToDelete);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateUser(UserInformation userInformation)
    {
        var mappedUser = _mapper.Map<UserInformation, User>(userInformation);
        var userToUpdate = await _context.Users
            .SingleAsync(u => u.Username == mappedUser.Username);
        mappedUser.Id = userToUpdate.Id;

        _context.Entry(userToUpdate).CurrentValues.SetValues(mappedUser);

        await _context.SaveChangesAsync();
    }
}
