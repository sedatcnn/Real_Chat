using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Dtos.JoinDtos
{
    public class RejectDto
    {
        public Guid RequestId { get; set; }
        public Guid RejectingUserId { get; set; }
    }
}
