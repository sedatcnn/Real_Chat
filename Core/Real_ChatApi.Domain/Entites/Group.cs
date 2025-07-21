using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Domain.Entites
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsPrivate { get; set; } = false;
        public Guid CreatedByUserId { get; set; }

        public User CreatedByUser { get; set; } = null!;

        public ICollection<GroupUser> Members { get; set; } = new List<GroupUser>();
        public ICollection<JoinRequest> JoinRequests { get; set; } = new List<JoinRequest>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }

}
