using MediatR;
using Real_ChatApi.Application.Features.MedaitR.Results.MessageResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Queries.MessageQueries
{
    public class GetMessagesQuery : IRequest<List<GetMessageResult>>
    {
        public Guid GroupId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public string? SearchText { get; set; }
    }
}
