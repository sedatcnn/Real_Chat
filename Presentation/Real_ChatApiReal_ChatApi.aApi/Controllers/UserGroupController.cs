using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Real_ChatApi.Application.Features.MedaitR.Commands.UserGroupCommand;
using Real_ChatApi.Application.Features.MedaitR.Queries.GetGroupUsersQueries;

namespace Real_ChatApiReal_ChatApi.aApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetUsersInGroup()
        {
            var result = await _mediator.Send(new GetGroupUsersQuery ());
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUserToGroup(AddUserToGroupCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveUserFromGroup(RemoveUserFromGroupCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
