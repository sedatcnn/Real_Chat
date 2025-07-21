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
    public class ApproveJoinRequestHandler : IRequestHandler<ApproveJoinRequestCommand, bool>
    {
        private readonly IRepository<JoinRequest> _joinRequestRepository;
        private readonly IRepository<GroupUser> _groupUserRepository;

        public ApproveJoinRequestHandler(IRepository<JoinRequest> joinRequestRepository, IRepository<GroupUser> groupUserRepository)
        {
            _joinRequestRepository = joinRequestRepository;
            _groupUserRepository = groupUserRepository;
        }

        public async Task<bool> Handle(ApproveJoinRequestCommand request, CancellationToken cancellationToken)
        {
            var joinRequest = await _joinRequestRepository.GetByIdAsync(request.RequestId);
            if (joinRequest == null || joinRequest.IsApproved != null)
                return false;


            joinRequest.IsApproved = true;
            joinRequest.ApproverUserId = request.ApprovingUserId;

            var groupUser = new GroupUser
            {
                GroupId = joinRequest.GroupId,
                UserId = joinRequest.RequesterUserId,
                IsAdmin = false,
                JoinedAt = DateTime.UtcNow
            };
            await _groupUserRepository.CreatAsync(groupUser);

            await _joinRequestRepository.UpdateAsync(joinRequest);
            return true;
        }
    }

}
