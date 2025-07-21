using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Results.GroupResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Queries.GroupQueries
{
    public record GetGroupsQuery() : IRequest<List<GetGroupResult>>;

}
