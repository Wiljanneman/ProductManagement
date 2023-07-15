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
            return BadRequest(ex);
        }

    }

    [HttpPost("authenticate")]
    public async Task<ActionResult<TokenResponseVM>> Endpoint1([FromBody] UserVM credentials)
    {
        try
        {
            var res = await Mediator.Send(new AuthenticateUserQuery { Credentials = credentials }, CancellationToken.None);
            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
