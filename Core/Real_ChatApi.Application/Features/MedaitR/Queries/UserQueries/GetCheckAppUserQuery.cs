using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Results.UserResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Queries.UserQueries
{
    public class GetCheckAppUserQuery : IRequest<GetCheckAppUserQueryResult>
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
