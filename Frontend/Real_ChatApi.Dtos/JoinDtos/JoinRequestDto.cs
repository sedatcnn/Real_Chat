using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Dtos.JoinDtos
{
    public class JoinRequestDto
    {
        public Guid GroupId { get; set; }
        public Guid RequestingUserId { get; set; }
        public Guid ApproverUserId { get; set; } // Ekle

    }
}
