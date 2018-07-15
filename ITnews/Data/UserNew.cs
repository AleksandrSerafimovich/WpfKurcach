using ITnews.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITnews.Data
{
    public class UserNew
    {
        public Guid NewId { get; set; }
        public New New { get; set; }

        public double? Rating { get; set; }

        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
