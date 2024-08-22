using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Contract.Responses.Sight
{
    public class SightResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Images { get; set; }
        public string Country { get; set; }
        public int? YearOfFoundation { get; set; }
        public string Description { get; set; }
    }
}
