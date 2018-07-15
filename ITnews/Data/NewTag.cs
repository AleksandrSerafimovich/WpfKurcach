using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITnews.Data
{
    public class NewTag
    {
        public Guid NewId { get; set; }
        public New New { get; set; }

        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
