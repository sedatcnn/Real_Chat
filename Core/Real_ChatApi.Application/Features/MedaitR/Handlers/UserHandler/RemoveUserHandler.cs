using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Commands.UserCommand;
using Real_ChatApi.Application.Features.MedaitR.Commands.UserGroupCommand;
using Real_ChatApi.Application.Interfaces;
using Real_ChatApi.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Handlers.UserHandler
{
    public class RemoveUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IRepository<User> _repository;

        public RemoveUserHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.UserId);
            if (user == null) return false;

            await _repository.RemoveAsync(user);
            return true;
        }
    }
}
