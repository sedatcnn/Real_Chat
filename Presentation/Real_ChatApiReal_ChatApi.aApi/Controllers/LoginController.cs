using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Real_ChatApi.Application.Features.MedaitR.Queries.UserQueries;
using Real_ChatApi.Application.Interfaces;
using Real_ChatApi.Application.Tools;
using Real_ChatApi.Domain.Entites;
using Real_ChatApi.Persistence.Repositories;

namespace Real_ChatApiReal_ChatApi.aApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly TokenService _tokenService;
        private readonly IRepository<User> _userRepository;
        public LoginController(IMediator mediator, TokenService tokenService, IRepository<User> userRepository)
        {
            _mediator = mediator;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(GetCheckAppUserQuery query)
        {
            var userResult = await _mediator.Send(query);

            if (!userResult.IsExist)
                return BadRequest("Email/Kullanıcı adı veya şifre hatalıdır.");

            var user = await _userRepository.GetByIdAsync(userResult.Id);
            var tokens = await _tokenService.GenerateTokensAsync(user);

            return Ok(new
            {
                Token = tokens.jwtToken,
                RefreshToken = tokens.refreshToken
            });
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return Unauthorized();

            var isValid = await _tokenService.ValidateRefreshTokenAsync(user.Id, request.RefreshToken);
            if (!isValid)
                return Unauthorized();

            var (jwtToken, newRefreshToken) = await _tokenService.GenerateTokensAsync(user);

            // Önceki refresh token iptal edilebilir (opsiyonel)
            await _tokenService.RevokeRefreshTokenAsync(request.RefreshToken);

            return Ok(new
            {
                Token = jwtToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}
public class RefreshRequest
{
    public Guid UserId { get; set; }
    public string RefreshToken { get; set; } = null!;
}