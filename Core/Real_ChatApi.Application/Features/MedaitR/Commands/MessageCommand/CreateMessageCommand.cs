using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Commands.MessageCommand
{
    public class CreateMessageCommand : IRequest<Guid>
    {
        public Guid SenderUserId { get; set; }
        public Guid GroupId { get; set; }
        public string Text { get; set; } = default!;
        public string? FileUrl { get; set; } // opsiyonel dosya
    }
}
