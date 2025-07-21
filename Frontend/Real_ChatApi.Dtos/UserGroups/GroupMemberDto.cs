using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Dtos.UserGroups
{
    public class GroupMemberDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
