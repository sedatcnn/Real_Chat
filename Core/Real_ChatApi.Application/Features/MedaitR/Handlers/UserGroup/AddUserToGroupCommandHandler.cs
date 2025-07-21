using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Commands.UserGroupCommand;
using Real_ChatApi.Application.Interfaces;
using Real_ChatApi.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Handlers.UserGroup
{
    public class AddUserToGroupCommandHandler : IRequestHandler<AddUserToGroupCommand>
    {
        private readonly IRepository<GroupUser> _repository;

        public AddUserToGroupCommandHandler(IRepository<GroupUser> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddUserToGroupCommand request, CancellationToken cancellationToken)
        {
            var groupUser = new GroupUser
            {
                GroupId = request.GroupId,
                UserId = request.UserId,
                IsAdmin = request.IsAdmin,
                JoinedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(groupUser);
            return Unit.Value;
        }
    }
}
