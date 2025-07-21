using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Results.JoinRequestResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Queries.JoinRequestQueries
{
    public record GetJoinRequestsQuery() : IRequest<List<GetJoinRequestResult>>;

}
