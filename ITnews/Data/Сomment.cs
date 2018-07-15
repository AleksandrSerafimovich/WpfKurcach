using ITnews.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITnews.Data
{
    public class Сomment
    {
        [Key]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid AuthorId { get; set; }

        public int Likes { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<UserComment> UserComments { get; set; }

        public Сomment()
        {
            UserComments = new List<UserComment>();
        }

        public Guid? NewId { get; set; }
        public virtual New New { get; set; }
    }
}
