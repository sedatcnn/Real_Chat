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
    public class RequestJoinGroupHandler : IRequestHandler<RequestJoinGroupCommand, Guid>
    {
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<JoinRequest> _joinRequestRepository;

        public RequestJoinGroupHandler(IRepository<Group> groupRepository, IRepository<JoinRequest> joinRequestRepository)
        {
            _groupRepository = groupRepository;
            _joinRequestRepository = joinRequestRepository;
        }

        public async Task<Guid> Handle(RequestJoinGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId);
            if (group == null || !group.IsPrivate)
                return Guid.Empty;

            // Zaten bekleyen bir istek var mı kontrolü
            var existingRequest = group.JoinRequests.FirstOrDefault(r => r.RequesterUserId == request.RequestingUserId && r.IsApproved == null);
            if (existingRequest != null)
                return existingRequest.Id;

            var joinRequest = new JoinRequest
            {
                Id = Guid.NewGuid(),
                GroupId = request.GroupId,
                RequesterUserId = request.RequestingUserId,
                RequestedAt = DateTime.UtcNow,
                IsApproved = null, // Beklemede
                    ApproverUserId = request.ApproverUserId, // Set ettik

            };

            await _joinRequestRepository.CreatAsync(joinRequest);
            return joinRequest.Id;
        }
    }

}
