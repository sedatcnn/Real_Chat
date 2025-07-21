using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Commands.UserGroupCommand
{
    public class RemoveUserFromGroupCommand : IRequest
    {
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
    }
}
