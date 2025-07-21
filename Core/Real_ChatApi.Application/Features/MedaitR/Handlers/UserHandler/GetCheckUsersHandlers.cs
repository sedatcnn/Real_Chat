using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Queries.UserQueries;
using Real_ChatApi.Application.Features.MedaitR.Results.UserResult;
using Real_ChatApi.Application.Interfaces;
using Real_ChatApi.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Handlers.UserHandler
{
    class GetCheckUsersHandlers : IRequestHandler<GetCheckAppUserQuery, GetCheckAppUserQueryResult>
    {
        private readonly IRepository<User> _appUserRepository;
        public GetCheckUsersHandlers(IRepository<User> appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }



        public async Task<GetCheckAppUserQueryResult> Handle(GetCheckAppUserQuery request, CancellationToken cancellationToken)
        {
            var values = new GetCheckAppUserQueryResult();
            var user = await _appUserRepository.GetByFilterAsync(x => x.PasswordHash == request.Password && x.Email == request.Mail);
            if (user == null)
            {
                values.IsExist = false;
            }
            else
            {
                values.IsExist = true;
                values.Id = user.Id;
                values.Mail = user.Email;

            }
            return values;
        }
    }
}
