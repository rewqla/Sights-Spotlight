using API.Contract.Requests.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Models
{
    public class GetAllSightsOptions
    {
        public string? Country { get; set; }
        public int? YearOfFoundationFrom { get; set; }
        public int? YearOfFoundationTo { get; set; }
        public string? SortField { get; set; }
    }
}
