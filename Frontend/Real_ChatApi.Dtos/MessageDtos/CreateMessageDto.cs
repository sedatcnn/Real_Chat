using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Dtos.MessageDtos
{
    public class CreateMessageDto
    {
        public Guid GroupId { get; set; }
        public Guid SenderUserId { get; set; }
        public string? Text { get; set; }
        public string? FileUrl { get; set; }
    }
}
