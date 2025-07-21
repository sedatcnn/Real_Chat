using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Commands.MessageCommand
{
    public class UpdateMessageCommand : IRequest<bool>
    {
        public Guid MessageId { get; set; }
        public Guid RequestingUserId { get; set; } // sadece mesajın sahibi düzenleyebilir
        public string NewContent { get; set; } = default!;
    }
}
