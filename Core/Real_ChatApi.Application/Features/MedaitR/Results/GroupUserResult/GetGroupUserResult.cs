using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Results.GroupUserResult
{
    public class GetGroupUserResult
    {
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
