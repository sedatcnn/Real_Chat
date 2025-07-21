using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Real_ChatApi.Application.Features.MedaitR.Commands.JoinRequestCommand;
using Real_ChatApi.Application.Features.MedaitR.Queries.JoinRequestQueries;

namespace Real_ChatApiReal_ChatApi.aApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoinGroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JoinGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-requests")]
        public async Task<IActionResult> GetJoinRequests(Guid groupId)
        {
            var requests = await _mediator.Send(new GetJoinRequestsQuery());
            return Ok(requests);
        }
        [HttpPost("request")]
        public async Task<IActionResult> RequestToJoin(RequestJoinGroupCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("join-public")]
        public async Task<IActionResult> JoinPublicGroup([FromBody] JoinPublicGroupCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
                return BadRequest("Public gruba katılamadı.");

            return Ok("Gruba başarıyla katıldın.");
        }
        [HttpPost("approve")]
        public async Task<IActionResult> ApproveRequest(ApproveJoinRequestCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("reject")]
        public async Task<IActionResult> RejectRequest(RejectJoinRequestCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
