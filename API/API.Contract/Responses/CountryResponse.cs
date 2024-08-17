using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Contract.Responses
{
    public class CountryResponse
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string ImageURL { get; set; }
    }
}
