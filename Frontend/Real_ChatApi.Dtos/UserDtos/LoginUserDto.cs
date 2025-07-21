using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Dtos.UserDtos
{
    public class LoginUserDto
    {
        public string Mail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
