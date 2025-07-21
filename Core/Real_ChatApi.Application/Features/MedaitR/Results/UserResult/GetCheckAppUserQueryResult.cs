using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Features.MedaitR.Results.UserResult
{
    public class GetCheckAppUserQueryResult
    {
        public Guid Id { get; set; }
        public bool IsExist { get; set; }
        public string Username { get; set; }
        public string Mail { get; set; }
    }
}
