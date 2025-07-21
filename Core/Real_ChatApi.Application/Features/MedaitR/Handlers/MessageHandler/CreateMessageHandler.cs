using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Commands.MessageCommand;
using Real_ChatApi.Application.Interfaces;
using Real_ChatApi.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Handlers.MessageHandler
{
    public class CreateMessageHandler : IRequestHandler<CreateMessageCommand, Guid>
    {
        private readonly IRepository<Message> _messageRepository;

        public CreateMessageHandler(IRepository<Message> messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Guid> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                GroupId = request.GroupId,
                SenderUserId = request.SenderUserId,
                Text = request.Text,
                FileUrl = request.FileUrl,
                SentAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _messageRepository.CreatAsync(message);

            return message.Id;
        }
    }

}
