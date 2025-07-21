using MediatR;
using Real_ChatApi.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Commands.GroupCommand
{
    public class DeleteGroupCommand : IRequest<bool>
    {
        public Guid GroupId { get; set; }
        public DeleteGroupCommand(Guid groupId)
        {
            GroupId = groupId;
        }
    }
}
