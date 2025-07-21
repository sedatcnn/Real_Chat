using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Commands.JoinRequestCommand;
using Real_ChatApi.Application.Interfaces;
using Real_ChatApi.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Handlers.JoinGroupHandler
{
    public class JoinPublicGroupHandler : IRequestHandler<JoinPublicGroupCommand, bool>
    {
        private readonly IRepository<Group> _groupRepo;
        private readonly IRepository<GroupUser> _groupUserRepo;

        public JoinPublicGroupHandler(
            IRepository<Group> groupRepo,
            IRepository<GroupUser> groupUserRepo)
        {
            _groupRepo = groupRepo;
            _groupUserRepo = groupUserRepo;
        }

        public async Task<bool> Handle(JoinPublicGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepo.GetByIdAsync(request.GroupId);
            if (group == null || group.IsPrivate)
                return false;

            var alreadyJoined = await _groupUserRepo.GetByFilterAsync(g =>
                g.GroupId == request.GroupId && g.UserId == request.UserId);

            if (alreadyJoined != null)
                return false;

            var groupUser = new GroupUser
            {
                GroupId = request.GroupId,
                UserId = request.UserId
            };

            await _groupUserRepo.CreatAsync(groupUser);
            return true;
        }

    }

}
