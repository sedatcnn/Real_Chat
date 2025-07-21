using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Commands.GroupCommand;
using Real_ChatApi.Application.Interfaces;
using Real_ChatApi.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Handlers.GroupHandler
{
    public class CreateGroupHandler : IRequestHandler<CreateGroupCommand, Guid>
    {
        private readonly IRepository<Group> _groupRepository;

        public CreateGroupHandler(IRepository<Group> groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new Group
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                IsPrivate = request.IsPrivate,
                CreatedByUserId = request.CreatedByUserId,
            };

            if (request.InitialMemberUserIds != null && request.InitialMemberUserIds.Any())
            {
                foreach (var userId in request.InitialMemberUserIds)
                {
                    group.Members.Add(new GroupUser
                    {
                        GroupId = group.Id,
                        UserId = userId,
                        IsAdmin = userId == request.CreatedByUserId,
                        JoinedAt = DateTime.UtcNow,
                    });
                }
            }
            else
            {
                group.Members.Add(new GroupUser
                {
                    GroupId = group.Id,
                    UserId = request.CreatedByUserId,
                    IsAdmin = true,
                    JoinedAt = DateTime.UtcNow,
                });
            }

            await _groupRepository.CreatAsync(group);
            return group.Id;
        }
    }

}
