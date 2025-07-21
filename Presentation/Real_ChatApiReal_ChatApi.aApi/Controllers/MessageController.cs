using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Real_ChatApi.Application.Features.MedaitR.Commands.MessageCommand;
using Real_ChatApi.Application.Features.MedaitR.Queries.MessageQueries;

namespace Real_ChatApiReal_ChatApi.aApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteMessage([FromBody] DeleteMessageCommand command)
        {
            var success = await _mediator.Send(command);
            if (!success)
                return BadRequest("Message not found or already deleted.");

            return Ok("Message deleted successfully.");
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditMessage([FromBody] UpdateMessageCommand command)
        {
            var success = await _mediator.Send(command);
            if (!success)
                return BadRequest("Message not found or already deleted.");

            return Ok("Message updated successfully.");
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("group")]
        public async Task<IActionResult> GetMessages()
        {
            var result = await _mediator.Send(new GetMessagesQuery());
            return Ok(result);
        }
    }
}
