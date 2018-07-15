using ITnews.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITnews.Models.NewViewModel
{
    public class NewsViewModel
    {
       [Required]
       public string Title { get; set; }
        [Required]
        public string Text { get; set; }

        [Required]
        public string Tags { get; set; }

    }
}
