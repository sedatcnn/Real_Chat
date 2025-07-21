using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Queries.GroupQueries;
using Real_ChatApi.Application.Features.MedaitR.Results.GroupResult;
using Real_ChatApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Handlers.GroupHandler
{
    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, List<GetGroupResult>>
    {
        private readonly IWithRepository _withRepository;

        public GetGroupsQueryHandler(IWithRepository withRepository)
        {
            _withRepository = withRepository;
        }

        public async Task<List<GetGroupResult>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = await _withRepository.GetGroupsAsync();

            var results = groups.Select(g => new GetGroupResult
            {
                Id = g.Id,
                Name = g.Name,
                IsPrivate = g.IsPrivate,
                CreatedByUserId = g.CreatedByUserId,
                MemberUserIds = g.Members.Select(m => m.UserId).ToList()
            }).ToList();

            return results;
        }

    }
}
