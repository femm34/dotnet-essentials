using System.Net;
using MiApi.Business;
using MiApi.Exceptions;
using MiApi.Models.DTOs;
using MiApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateUserById([FromRoute] int id, [FromBody] UpdateUserDto user)
        {
            try
            {
                var userUpdated = _userRepository.UpdateUser(id, user);

                return Ok(BuilderApiResponse<UserDto>.Build("User updated successfully.",
                    HttpStatusCode.OK.GetDisplayName(), userUpdated, HttpStatusCode.OK.GetHashCode()));
            }
            catch (UsersNotFoundException ex)
            {
                return NotFound(BuilderApiResponse<object>.Build(ex.Message, HttpStatusCode.NotFound.GetDisplayName(),
                    null, HttpStatusCode.NotFound.GetHashCode()));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(),
                    BuilderApiResponse<object>.Build(ex.Message, HttpStatusCode.InternalServerError.GetDisplayName(),
                        null, HttpStatusCode.InternalServerError.GetHashCode()));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserById([FromRoute] int id)
        {
            try
            {
                _userRepository.DeleteByUserId(id);

                return Ok(BuilderApiResponse<object>.Build("User deleted successfully.",
                    HttpStatusCode.OK.GetDisplayName(), null, HttpStatusCode.OK.GetHashCode()));
            }
            catch (UsersNotFoundException ex)
            {
                return NotFound(BuilderApiResponse<object>.Build(ex.Message, HttpStatusCode.NotFound.GetDisplayName(),
                    null, HttpStatusCode.NotFound.GetHashCode()));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(),
                    BuilderApiResponse<object>.Build(ex.Message, HttpStatusCode.InternalServerError.GetDisplayName(),
                        null, HttpStatusCode.InternalServerError.GetHashCode()));
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetUserById([FromRoute] int id)
        {
            try
            {
                var user = _userRepository.GetUserById(id);

                return Ok(BuilderApiResponse<object>.Build("User fetched Successfully.",
                    HttpStatusCode.OK.GetDisplayName(), user, HttpStatusCode.OK.GetHashCode()));
            }
            catch (UsersNotFoundException ex)
            {
                return NotFound(BuilderApiResponse<object>.Build(ex.Message, HttpStatusCode.NotFound.GetDisplayName(),
                    null, HttpStatusCode.NotFound.GetHashCode()));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(),
                    BuilderApiResponse<object>.Build(ex.Message, HttpStatusCode.InternalServerError.GetDisplayName(),
                        null, HttpStatusCode.InternalServerError.GetHashCode()));
            }
        }

        [HttpGet]
        // [ApiResponseMessageAtribute("Fetched all users successfully.")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userRepository.GetAllUsers();

                return Ok(BuilderApiResponse<object>.Build("Users fetched Successfully.",
                    HttpStatusCode.OK.GetDisplayName(), users, HttpStatusCode.OK.GetHashCode()));
            }
            catch (UsersNotFoundException ex)
            {
                return NotFound(BuilderApiResponse<object>.Build(ex.Message, HttpStatusCode.NotFound.GetDisplayName(),
                    null, HttpStatusCode.NotFound.GetHashCode()));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(),
                    BuilderApiResponse<object>.Build(ex.Message, HttpStatusCode.InternalServerError.GetDisplayName(),
                        null, HttpStatusCode.InternalServerError.GetHashCode()));
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserDto user)
        {
            try
            {
                UserDto UserCreated = _userRepository.CreateUser(user);

                return Ok(BuilderApiResponse<UserDto>.Build("User created successfully.",
                    HttpStatusCode.OK.GetDisplayName(), UserCreated, HttpStatusCode.OK.GetHashCode()));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(),
                    BuilderApiResponse<object>.Build(ex.Message, HttpStatusCode.InternalServerError.GetDisplayName(),
                        null, HttpStatusCode.InternalServerError.GetHashCode()));
            }
        }
    }
}