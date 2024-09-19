using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Entities
{
    public sealed class SightPhoto : BaseEntity
    {
        public string Url { get; set; }
        public int SightId { get; set; }
        public Sight Sight { get; set; }
    }
}
