using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Contract.Responses
{
    public class UserResponse
    {
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}
