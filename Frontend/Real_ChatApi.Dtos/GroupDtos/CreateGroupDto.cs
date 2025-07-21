using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Dtos.GroupDtos
{
    public class CreateGroupDto
    {
        public string Name { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
        public Guid CreatedByUserId { get; set; }
        public List<Guid>? InitialMemberUserIds { get; set; } = new();
    }


}
