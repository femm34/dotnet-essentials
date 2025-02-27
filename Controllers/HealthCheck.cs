using Microsoft.AspNetCore.Mvc;

namespace MiApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthCheckController : ControllerBase

{
    [HttpGet]
    public ActionResult<string> Get() => Ok("Healthy!");

    [HttpGet("ping")]
    public string Ping() => "Pong!";

    [HttpGet("notfound")]
    public ActionResult<string> NotFoundExample()
    {
        return NotFound("Resource not found");
    }

    [HttpGet("badrequest")]
    public ActionResult<string> BadRequestExample()
    {
        return BadRequest("Invalid request");
    }

    [HttpGet("customstatus")]
    public ActionResult<string> CustomStatus()
    {
        return StatusCode(418, "I'm a teapot");
    }
}