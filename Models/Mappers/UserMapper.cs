using MiApi.Models.DTOs;

namespace MiApi.Models.Mappers;

public abstract class UserMapper
{
    public static User ToUser(CreateUserDto userDto)
    {
        return new User()
        {
            Username = userDto.Username,
            Email = userDto.Email,
            Name = userDto.Name,
            FirstLastName = userDto.FirstLastName,
            SecondLastName = userDto.SecondLastName
        };
    }
    
    public static UserDto ToUserDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Name = user.Name,
            FirstLastName = user.FirstLastName,
            SecondLastName = user.SecondLastName
        };
    }
}