using ITnews.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITnews.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

        public virtual ICollection<UserNew> UserNew { get; set; }

        public virtual ICollection<UserComment> UserComments { get; set; }

        public ApplicationUser()
        {
            UserComments = new List<UserComment>();
            UserNew = new List<UserNew>();
        }

        public string RoleName { get; set; }
    }
}
