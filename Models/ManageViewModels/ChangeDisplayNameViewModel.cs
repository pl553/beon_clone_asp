using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Beon.Models.ManageViewModels
{
    public class ChangeDisplayNameViewModel
    {
        [Required]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "Display name")]
        public string DisplayName { get; set; }
    }
}
