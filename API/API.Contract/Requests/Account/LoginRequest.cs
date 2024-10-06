using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Contract.Requests.Account
{
    public record LoginRequest
    {
        public required string Username { get; init; }
        public required string Password { get; init; }
    }
}
