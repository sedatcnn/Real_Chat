using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Commands.GroupCommand;
using Real_ChatApi.Application.Interfaces;
using Real_ChatApi.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Handlers.GroupHandler
{
    public class UpdateGroupHandler : IRequestHandler<UpdateGroupCommand, bool>
    {
        private readonly IRepository<Group> _groupRepository;

        public UpdateGroupHandler(IRepository<Group> groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<bool> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.Id);
            if (group == null)
                return false;

            if (!string.IsNullOrEmpty(request.Name))
                group.Name = request.Name;

            if (request.IsPrivate.HasValue)
                group.IsPrivate = request.IsPrivate.Value;

            await _groupRepository.UpdateAsync(group);
            return true;
        }
    }
}
