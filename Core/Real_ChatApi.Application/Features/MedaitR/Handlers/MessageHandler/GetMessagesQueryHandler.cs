using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Queries.MessageQueries;
using Real_ChatApi.Application.Features.MedaitR.Results.MessageResult;
using Real_ChatApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Handlers.MessageHandler
{
    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, List<GetMessageResult>>
    {
        private readonly IWithRepository _withRepository;

        public GetMessagesQueryHandler(IWithRepository withRepository)
        {
            _withRepository = withRepository;
        }

        public async Task<List<GetMessageResult>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            // Mesajları repository'den GroupId bazında al
            var allMessages = await _withRepository.GetMessagesByGroupAsync(request.GroupId);

            // Eğer searchText varsa filtrele
            if (!string.IsNullOrEmpty(request.SearchText))
            {
                allMessages = allMessages
                    .Where(m => m.Content != null && m.Content.Contains(request.SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Sayfalama uygula
            var pagedMessages = allMessages
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var results = pagedMessages.Select(m => new GetMessageResult
            {
                Id = m.Id,
                SenderUserId = m.SenderUserId,
                GroupId = m.GroupId,
                Content = m.Content,
                SentAt = m.SentAt,
                IsEdited = m.EditedAt.HasValue,
                IsDeleted = m.IsDeleted
            }).ToList();

            return results;
        }

    }
}
