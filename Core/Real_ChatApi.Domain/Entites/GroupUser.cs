using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Domain.Entites
{
    public class GroupUser
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public bool IsAdmin { get; set; } = false;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }

}
