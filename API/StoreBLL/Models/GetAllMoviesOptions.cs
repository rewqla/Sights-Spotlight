using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Models
{
    public class GetAllMoviesOptions
    {
        public string? Country { get; set; }
        public int? YearOfFoundationFrom { get; set; }
        public int? YearOfFoundationTo { get; set; }
        public string? SortField { get; set; }
    }
}
