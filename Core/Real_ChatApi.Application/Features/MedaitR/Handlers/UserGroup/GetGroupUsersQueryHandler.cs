using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Queries.GetGroupUsersQueries;
using Real_ChatApi.Application.Features.MedaitR.Queries.GroupQueries;
using Real_ChatApi.Application.Features.MedaitR.Results.GroupResult;
using Real_ChatApi.Application.Features.MedaitR.Results.GroupUserResult;
using Real_ChatApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Handlers.UserGroup
{
    public class GetGroupUsersQueryHandler : IRequestHandler<GetGroupUsersQuery, List<GetGroupUserResult>>
    {
        private readonly IWithRepository _withRepository;

        public GetGroupUsersQueryHandler(IWithRepository withRepository)
        {
            _withRepository = withRepository;
        }

        public async Task<List<GetGroupUserResult>> Handle(GetGroupUsersQuery request, CancellationToken cancellationToken)
        {
            var groupUsers = await _withRepository.GetGroupUsersAsync();

            var results = groupUsers.Select(gu => new GetGroupUserResult
            {
                GroupId = gu.GroupId,
                UserId = gu.UserId,
                IsAdmin = gu.IsAdmin,
                JoinedAt = gu.JoinedAt
            }).ToList();

            return results;
        }

    }
}
