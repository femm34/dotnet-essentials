using MiApi.Business;
using MiApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MiApi.Exceptions;
using MiApi.Utils;
using Microsoft.OpenApi.Extensions;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthController> _logger;
        
        public AuthController(IUserRepository userRepository, ILogger<AuthController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            try
            {
                _logger.LogInformation("Logging in user with username: {0}", login.Username);
                Token token = _userRepository.Login(login).Result;

                return Ok(BuilderApiResponse<Token>.Build("User logged in successfully.",
                    HttpStatusCode.OK.GetDisplayName(), token, HttpStatusCode.OK.GetHashCode()));
            }
            catch (UsersNotFoundException ex)
            {
                return NotFound(BuilderApiResponse<object>.Build(ex.Message, HttpStatusCode.NotFound.GetDisplayName(),
                    null, HttpStatusCode.NotFound.GetHashCode()));
            }
            catch (InvalidPasswordException ex)
            {
                return BadRequest(BuilderApiResponse<object>.Build(ex.Message, HttpStatusCode.BadRequest.GetDisplayName(),
                    null, HttpStatusCode.BadRequest.GetHashCode()));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(),
                    BuilderApiResponse<object>.Build(ex.Message, HttpStatusCode.InternalServerError.GetDisplayName(),
                        null, HttpStatusCode.InternalServerError.GetHashCode()));
            }
            
        }
        
        [HttpPost("register")]
        public IActionResult RegisterUSer([FromBody] CreateUserDto user)
        {
            try
            {
                _logger.LogInformation("Creating user with username: {0}", user.Username);   
                UserDto UserCreated = _userRepository.RegisterUser(user);

                return Ok(BuilderApiResponse<UserDto>.Build("User created successfully.",
                    HttpStatusCode.OK.GetDisplayName(), UserCreated, HttpStatusCode.OK.GetHashCode()));
            }
            catch (UserAlreadyExistsException ex)
            {
                return BadRequest(BuilderApiResponse<object>.Build(ex.Message, HttpStatusCode.BadRequest.GetDisplayName(),
                    null, HttpStatusCode.BadRequest.GetHashCode()));
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