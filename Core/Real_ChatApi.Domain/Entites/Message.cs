using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Domain.Entites
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public Guid SenderUserId { get; set; }
        public User SenderUser { get; set; } = null!;

        public string? Text { get; set; }
        public string? FileUrl { get; set; }

        public bool IsDeleted { get; set; } = false; // Soft delete
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public DateTime? EditedAt { get; set; }
    }

}
