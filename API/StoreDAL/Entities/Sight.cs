using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Entities
{
    public class Sight
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public IList<SightPhoto> SightPhotos { get; set; }
    }
}
