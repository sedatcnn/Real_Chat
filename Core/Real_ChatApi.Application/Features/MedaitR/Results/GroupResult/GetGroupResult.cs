using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Results.GroupResult
{
    public class GetGroupResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
        public Guid CreatedByUserId { get; set; }
        public List<Guid> MemberUserIds { get; set; } = new();
    }
}
