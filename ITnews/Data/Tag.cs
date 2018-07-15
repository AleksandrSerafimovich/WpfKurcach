using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITnews.Data
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<NewTag> NewTag { get; set; }

        public Tag()
        {
            NewTag = new List<NewTag>();
        }
    }
}
