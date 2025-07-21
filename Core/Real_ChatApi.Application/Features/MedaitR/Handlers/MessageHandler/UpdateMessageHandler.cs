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
    public class UpdateMessageHandler : IRequestHandler<UpdateMessageCommand, bool>
    {
        private readonly IRepository<Message> _messageRepository;

        public UpdateMessageHandler(IRepository<Message> messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<bool> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _messageRepository.GetByIdAsync(request.MessageId);
            if (message == null || message.SenderUserId != request.RequestingUserId)
                return false;

            message.Text = request.NewContent;
            message.EditedAt = DateTime.UtcNow;

            await _messageRepository.UpdateAsync(message);
            return true;
        }
    }

}
