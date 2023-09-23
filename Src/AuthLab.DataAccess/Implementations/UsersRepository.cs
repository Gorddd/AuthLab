using AuthLab.DataAccess.EfCore;
using AuthLab.Domain.Abstractions;
using AuthLab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AuthLab.DataAccess.Models;

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
    }

    public async Task<UserInformation> GetUserByName(string username)
    {
        return await _context.Users.ProjectTo<UserInformation>(_mapper.ConfigurationProvider)
            .SingleAsync(u => u.Username == username);
    }

    public async Task<IEnumerable<UserInformation>> GetUsers()
    {
        return await _context.Users.ProjectTo<UserInformation>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<bool> IsThereUser(string username)
    {
        return await _context.Users.ProjectTo<UserInformation>(_mapper.ConfigurationProvider)
            .AnyAsync(u => u.Username == username);
    }

    public async Task UpdateUser(UserInformation userInformation)
    {
        _context.Users.Update(_mapper.Map<UserInformation, User>(userInformation));

        await _context.SaveChangesAsync();
    }
}
