using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Commands.UserCommand
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public DeleteUserCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
