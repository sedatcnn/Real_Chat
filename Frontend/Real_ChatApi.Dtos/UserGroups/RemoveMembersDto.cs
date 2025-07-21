using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Dtos.UserGroups
{
     public class RemoveMembersDto
    {
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
    }
}
