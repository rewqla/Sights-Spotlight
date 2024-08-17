using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Contract.Requests
{
    public class CreateCountryRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string MainImageURL { get; set; }
        public required string SecondaryImageURL { get; set; }
    }
}
