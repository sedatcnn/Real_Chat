using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Dtos.JoinDtos
{
    public class JoinPublicDto
    {
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
    }
}
