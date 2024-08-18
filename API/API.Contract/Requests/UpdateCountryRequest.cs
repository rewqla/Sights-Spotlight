using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Contract.Requests
{
    public class UpdateCountryRequest : CreateCountryRequest
    {
        public required int Id { get; set; }
    }
}
