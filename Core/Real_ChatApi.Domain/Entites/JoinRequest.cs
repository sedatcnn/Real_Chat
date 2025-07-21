using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Domain.Entites
{
    public class JoinRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public Guid RequesterUserId { get; set; } // isteği atan
        public User RequesterUser { get; set; } = null!;

        public Guid ApproverUserId { get; set; } // onaylayacak olan (admin)
        public User ApproverUser { get; set; } = null!;

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public bool? IsApproved { get; set; } = null; // null: bekliyor, true: kabul, false: red
    }

}
