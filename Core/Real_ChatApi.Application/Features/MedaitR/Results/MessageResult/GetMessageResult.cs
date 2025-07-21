using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Results.MessageResult
{
    public class GetMessageResult
    {
        public Guid Id { get; set; }
        public Guid SenderUserId { get; set; }
        public Guid GroupId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public bool IsEdited { get; set; }
        public bool IsDeleted { get; set; }
    }
}
