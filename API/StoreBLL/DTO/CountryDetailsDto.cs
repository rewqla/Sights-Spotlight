using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.DTO
{
    public class CountryDetailsDto : CountryDto
    {
        public string Description { get; set; }
        public List<CountrySightDto> CountrySightDtos { get; set; }
    }
}
