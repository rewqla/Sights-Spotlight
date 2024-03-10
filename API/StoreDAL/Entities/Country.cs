using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MainImgaeURL { get; set; }
        public string SecondaryImageURL { get; set; }
        public IList<Sight> Sights { get; set; }
    }
}
