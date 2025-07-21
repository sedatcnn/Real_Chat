using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Results.GroupUserResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Queries.GetGroupUsersQueries
{
    public record GetGroupUsersQuery() : IRequest<List<GetGroupUserResult>>;

}
