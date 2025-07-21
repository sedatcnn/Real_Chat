using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Commands.GroupCommand
{
    public class CreateGroupCommand : IRequest<Guid>
    {
        public string Name { get; set; } = default!;
        public bool IsPrivate { get; set; }
        public Guid CreatedByUserId { get; set; }
        public List<Guid>? InitialMemberUserIds { get; set; }
    }
}
