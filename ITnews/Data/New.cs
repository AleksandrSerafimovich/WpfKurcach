using ITnews.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITnews.Data
{
    public class New
    {
        [Key]
        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public double Rating { get; set; }

        public virtual ICollection<UserNew> UserNew { get; set; }

        public virtual ICollection<Сomment> Comments { get; set; }

        public New()
        {
            UserNew = new List<UserNew>();
            Comments = new List<Сomment>();
        }

    }
}
