using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Dtos.GroupDtos
{
    public class GetGroupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsPrivate { get; set; }
        public Guid CreatedByUserId { get; set; }
    }
}
