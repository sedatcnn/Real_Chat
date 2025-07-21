using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Domain.Entites
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "User"; // Eklenen alan
        public ICollection<GroupUser> Groups { get; set; } = new List<GroupUser>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
