using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Entities
{
    public sealed class Country : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MainImageURL { get; set; }
        public string SecondaryImageURL { get; set; }
        public IList<Sight> Sights { get; set; }
    }
}