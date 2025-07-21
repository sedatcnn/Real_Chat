using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Commands.UserCommand;
using Real_ChatApi.Application.Interfaces;
using Real_ChatApi.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Handlers.UserHandler
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IRepository<User> _userRepository;

        public CreateUserHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.Username,
                Email = request.Email,
                PasswordHash = request.Password, 
                Role = request.Role
            };

            await _userRepository.CreatAsync(user);

            return user.Id;
        }
    }

}
