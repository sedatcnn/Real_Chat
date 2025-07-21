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
    public class DeleteGroupHandler : IRequestHandler<DeleteGroupCommand, bool>
    {
        private readonly IRepository<Group> _groupRepository;

        public DeleteGroupHandler(IRepository<Group> groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<bool> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId);
            if (group == null)
                return false;

            await _groupRepository.RemoveAsync(group);
            return true;
        }
    }
}
