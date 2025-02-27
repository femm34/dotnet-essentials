using Microsoft.AspNetCore.Mvc;

namespace MiApi.Users.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> GetUsers() => Ok("Healthy!");   
}