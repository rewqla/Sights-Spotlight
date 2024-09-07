using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Contract.Responses.Account
{
    public record UserResponse
    {
        public required string Email { get; init; }
        public required string Token { get; init; }
    }
}
