using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Real_ChatApi.Application.Features.MedaitR.Commands.UserCommand;

namespace Real_ChatApiReal_ChatApi.aApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegisterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreatUser(CreateUserCommand creat)
        {
            await _mediator.Send(creat);
            return Ok("Kullanıcı oluşturuldu");
        }
    }
}
