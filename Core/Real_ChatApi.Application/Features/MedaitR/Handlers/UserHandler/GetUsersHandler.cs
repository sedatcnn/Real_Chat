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
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserResult>>
    {
        private readonly IWithRepository _repository;

        public GetUsersHandler(IWithRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserResult>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetUsersAsync();

            return users.Select(u => new UserResult
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                Role = u.Role.ToString()
            }).ToList();
        }
    
}
}
