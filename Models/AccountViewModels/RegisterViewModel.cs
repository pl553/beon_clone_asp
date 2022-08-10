using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Beon.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(Beon.Settings.UserName.MaxLength)]
        [MinLength(Beon.Settings.UserName.MinLength)]
        [DataType(DataType.Text)]
        [RegularExpression(Beon.Settings.UserName.Regex)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(Beon.Settings.Password.MaxLength,
            ErrorMessage = "The {0} must be at least {2} characters long.",
            MinimumLength = Beon.Settings.Password.MinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
