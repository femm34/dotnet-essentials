using MiApi.Models;
using MiApi.Models.DTOs;

namespace MiApi.Business;

public interface IUserRepository
{
    List<UserDto> GetAllUsers();
    
    void DeleteByUserId(int id);
    
    UserDto GetUserById(int id);
    UserDto CreateUser(CreateUserDto userDto);
    
    UserDto UpdateUser(int id, UpdateUserDto userDto);
}