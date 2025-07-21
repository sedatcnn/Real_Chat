using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Commands.JoinRequestCommand
{
    public class RequestJoinGroupCommand : IRequest<Guid>
    {
        public Guid GroupId { get; set; }
        public Guid RequestingUserId { get; set; }
        public Guid ApproverUserId { get; set; } // Ekle

    }
}
