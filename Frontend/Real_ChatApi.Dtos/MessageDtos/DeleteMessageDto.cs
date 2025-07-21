using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Dtos.MessageDtos
{
    public class DeleteMessageDto
    {
        public Guid MessageId { get; set; }
        public Guid RequestingUserId { get; set; }
    }
}
