using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Results.UserResult;
using Real_ChatApi.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Queries.UserQueries
{
    public class GetUsersQuery : IRequest<List<UserResult>>
    {
    }
}
