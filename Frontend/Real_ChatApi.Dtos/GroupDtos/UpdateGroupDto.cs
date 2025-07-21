using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Dtos.GroupDtos
{
    public class UpdateGroupDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool? IsPrivate { get; set; }
    }
}
