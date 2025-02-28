using MiApi.Exceptions;
using MiApi.Models;
using MiApi.Models.DTOs;
using MiApi.Models.Mappers;
using MiApi.Persistence;
using MiApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace MiApi.Business.impl;

public class UserRepository : IUserRepository
{
    private readonly IConfiguration _configuration;
    private readonly JwtUtils _jwtUtils;
    private readonly ILogger<UserRepository> _logger;
    private readonly FernandoDbContext _fernandoDbContext;

    public UserRepository(IConfiguration configuration, FernandoDbContext fernandoDbContext,
        ILogger<UserRepository> logger, JwtUtils jwtUtils)
    {
        _configuration = configuration;
        _fernandoDbContext = fernandoDbContext;
        _logger = logger;
        _jwtUtils = jwtUtils;
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


    public UserDto RegisterUser(CreateUserDto userDto)
    {
        var userExists = _fernandoDbContext.Usuarios.Any(user => user.Username == userDto.Username);

        if (userExists)
        {
            throw new UserAlreadyExistsException();
        }
        userDto.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        var user = UserMapper.ToUser(userDto);

        _fernandoDbContext.Usuarios.Add(user);
        _fernandoDbContext.SaveChanges();

        return UserMapper.ToUserDto(user);
    }
    
    
    public async Task<Token> Login(LoginRequest loginRequest)
    {
        var userFound = await _fernandoDbContext.Usuarios
            .FirstOrDefaultAsync(user => user.Username == loginRequest.Username);

        if (userFound == null)
        {
            throw new UsersNotFoundException();
        }
        
        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, userFound.Password))
        {
            throw new InvalidPasswordException();
        }
        
        return new Token
        {
            AccessToken = _jwtUtils.GenerateJwtToken(userFound.Username)
        };

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