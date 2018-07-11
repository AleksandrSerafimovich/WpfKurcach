using ITnews.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITnews.Data
{
    public class UserComment
    {
        public Guid СommentId { get; set; }
        public Сomment Comment { get; set; }

        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
