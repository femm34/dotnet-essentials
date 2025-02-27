using MiApi.Exceptions;
using MiApi.Models;
using MiApi.Models.DTOs;
using MiApi.Models.Mappers;
using MiApi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MiApi.Business.impl;

public class UserRepository : IUserRepository
{
    private readonly IConfiguration _configuration;
    private readonly FernandoDbContext _fernandoDbContext;

    public UserRepository(IConfiguration configuration, FernandoDbContext fernandoDbContext)
    {
        _configuration = configuration;
        _fernandoDbContext = fernandoDbContext;
    }

    public void DeleteByUserId(int id)
    {
        _fernandoDbContext.Usuarios.Remove(_fernandoDbContext.Usuarios.Find(id) ?? throw new UsersNotFoundException());
        _fernandoDbContext.SaveChanges();
        }

    public UserDto GetUserById(int id)
    {
        User userFound = _fernandoDbContext.Usuarios.Find(id)
                         ?? throw new UsersNotFoundException();

        return UserMapper.ToUserDto(userFound);
    }


    public UserDto CreateUser(CreateUserDto userDto)
    {
        var user = UserMapper.ToUser(userDto);

        _fernandoDbContext.Usuarios.Add(user);
        _fernandoDbContext.SaveChanges();

        return UserMapper.ToUserDto(user);
    }

    public UserDto UpdateUser(int id, UpdateUserDto userDto)
    {
        var userFound = _fernandoDbContext.Usuarios.Find(id)
                        ?? throw new UsersNotFoundException();

        userFound.Username = userDto.Username;
        userFound.Password = userDto.Password;
        userFound.Email = userDto.Email;
        userFound.Name = userDto.Name;
        userFound.FirstLastName = userDto.FirstLastName;
        userFound.SecondLastName = userDto.SecondLastName;

        _fernandoDbContext.SaveChanges();

        return UserMapper.ToUserDto(userFound);
    }

    public List<UserDto> GetAllUsers()
    {
        var users = _fernandoDbContext.Usuarios
            .Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Name = u.Name,
                FirstLastName = u.FirstLastName,
                SecondLastName = u.SecondLastName
            })
            .ToList();

        if (users == null || !users.Any())
        {
            throw new UsersNotFoundException();
        }

        return users;
    }
}