using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
{
    //[HttpPost]
    //public async Task<ActionResult<int>> Create(CreateTodoListCommand command)
    //{
    //    return await Mediator.Send(command);
    //}
}
