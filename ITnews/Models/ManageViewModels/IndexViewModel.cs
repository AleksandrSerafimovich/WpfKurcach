using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITnews.Models.ManageViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Имя")]
        [RegularExpression(@"^[A-Za-zА-Яа-я]+$", ErrorMessage = "Введите корректное имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Введите корректное имя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [RegularExpression(@"^[A-Za-zА-Яа-я]+$", ErrorMessage = "Введите корректную фамилию")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Введите корректную фамилию")]
        public string Surname { get; set; }

        [Display(Name = "Возраст")]
        [Range(0, 110, ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }

        [Display(Name = "Город")]
        [RegularExpression(@"^[A-Za-zА-Яа-я]+$", ErrorMessage = "Введите корректный город")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Введите корректный город")]
        public string City { get; set; }

        [Phone(ErrorMessage ="PhoneError")]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
