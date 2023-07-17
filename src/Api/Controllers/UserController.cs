using Application.Users.Commands;
using Application.Users.Common;
using Application.Users.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
{

    private readonly ILogger<UserController> _logger;
    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(UserVM user)
    {
        try
        {
            var res = await Mediator.Send(new AddUserCommand { User = user }, CancellationToken.None);
            return Ok(res);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error on users");
            return BadRequest(ex.Message);
        }

    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<TokenResponseVM>> AuthenticateUser([FromBody] UserVM credentials)
    {
        try
        {
            var res = await Mediator.Send(new AuthenticateUserQuery { Credentials = credentials }, CancellationToken.None);
            return Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on users");
            return BadRequest(ex.Message);

        }
    }
}
