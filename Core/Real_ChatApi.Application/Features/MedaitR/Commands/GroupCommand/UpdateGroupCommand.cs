using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Commands.GroupCommand
{
    public class UpdateGroupCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool? IsPrivate { get; set; }
    }
}
