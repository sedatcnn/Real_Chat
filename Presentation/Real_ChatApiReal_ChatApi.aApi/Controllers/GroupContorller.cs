using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Real_ChatApi.Application.Features.MedaitR.Commands.GroupCommand;
using Real_ChatApi.Application.Features.MedaitR.Queries.GroupQueries;

namespace Real_ChatApiReal_ChatApi.aApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupContorller : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupContorller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]

        public async Task<IActionResult> GetGroups()
        {
            var groups = await _mediator.Send(new GetGroupsQuery());
            return Ok(groups);
        }

        [HttpPost("creat")]
        public async Task<IActionResult> CreateGroup(CreateGroupCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(Guid id, UpdateGroupCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Group ID mismatch.");
            }
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            var result = await _mediator.Send(new DeleteGroupCommand(id));
            return Ok(result);
        }
    }
}
