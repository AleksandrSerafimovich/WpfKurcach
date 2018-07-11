using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITnews.Models.AccountViewModels
{
    public class RegisterViewModel
    {
            [Display(Name = "UserName")]
            [Required(ErrorMessage = "UserNameRequired")]
            [StringLength(20, ErrorMessage = "UserNameLength", MinimumLength = 3)]
            public string UserName { get; set; }            

            [Display(Name = "Email")]
            [Required(ErrorMessage = "EmailRequired")]
            [EmailAddress(ErrorMessage = "EmailAddress")]
            public string Email { get; set; }

            [Display(Name = "Password")]
            [Required(ErrorMessage = "PasswordRequired")]
            [StringLength(20, ErrorMessage = "PasswordLength", MinimumLength = 6)]
            [DataType(DataType.Password, ErrorMessage = "Password")]
            public string Password { get; set; }

            [Display(Name = "Confirm")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "ConfirmError")]
            public string ConfirmPassword { get; set; }
    }
}
