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
    public class RemoveUserFromGroupCommandHandler : IRequestHandler<RemoveUserFromGroupCommand>
    {
        private readonly IRepository<GroupUser> _repository;

        public RemoveUserFromGroupCommandHandler(IRepository<GroupUser> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(RemoveUserFromGroupCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByFilterAsync(x => x.GroupId == request.GroupId && x.UserId == request.UserId);
            if (existing != null)
            {
                await _repository.RemoveAsync(existing);
            }

            return Unit.Value;
        }
    }
}
