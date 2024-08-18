using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Contract.Requests.Sight
{
    public class GetAllSightsRequest
    {
        public string? Country { get; set; }
        public string? Name { get; set; }
    }
}
