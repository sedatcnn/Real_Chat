using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Queries.JoinRequestQueries;
using Real_ChatApi.Application.Features.MedaitR.Results.JoinRequestResult;
using Real_ChatApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Handlers.JoinGroupHandler
{
    public class GetJoinRequestsQueryHandler : IRequestHandler<GetJoinRequestsQuery, List<GetJoinRequestResult>>
    {
        private readonly IWithRepository _withRepository;

        public GetJoinRequestsQueryHandler(IWithRepository withRepository)
        {
            _withRepository = withRepository;
        }

        public async Task<List<GetJoinRequestResult>> Handle(GetJoinRequestsQuery request, CancellationToken cancellationToken)
        {
            var joinRequests = await _withRepository.GetJoinRequestsAsync();

            var results = joinRequests.Select(j => new GetJoinRequestResult
            {
                Id = j.Id,
                GroupId = j.GroupId,
                UserId = j.RequesterUserId,
                IsApproved = j.IsApproved ?? false,
                RequestedAt = j.RequestedAt
            }).ToList();

            return results;
        }

    }
}
