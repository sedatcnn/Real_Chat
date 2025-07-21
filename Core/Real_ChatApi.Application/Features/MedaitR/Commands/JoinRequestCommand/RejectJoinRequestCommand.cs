using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Commands.JoinRequestCommand
{
    public class RejectJoinRequestCommand : IRequest<bool>
    {
        public Guid RequestId { get; set; }
        public Guid RejectingUserId { get; set; }
    }
}
