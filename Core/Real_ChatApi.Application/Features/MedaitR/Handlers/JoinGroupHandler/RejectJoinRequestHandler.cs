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
    public class RejectJoinRequestHandler : IRequestHandler<RejectJoinRequestCommand, bool>
    {
        private readonly IRepository<JoinRequest> _joinRequestRepository;

        public RejectJoinRequestHandler(IRepository<JoinRequest> joinRequestRepository)
        {
            _joinRequestRepository = joinRequestRepository;
        }

        public async Task<bool> Handle(RejectJoinRequestCommand request, CancellationToken cancellationToken)
        {
            var joinRequest = await _joinRequestRepository.GetByIdAsync(request.RequestId);
            if (joinRequest == null || joinRequest.IsApproved != null)
                return false;


            joinRequest.IsApproved = false;
            joinRequest.ApproverUserId = request.RejectingUserId;

            await _joinRequestRepository.UpdateAsync(joinRequest);
            return true;
        }
    }

}
