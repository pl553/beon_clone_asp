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
        [MaxLength(Beon.Settings.DisplayName.MaxLength)]
        [MinLength(Beon.Settings.DisplayName.MinLength)]
        [RegularExpression(Beon.Settings.DisplayName.Regex)]
        [Display(Name = "Display name")]
        [DataType(DataType.Text)]
        public string DisplayName { get; set; }
    }
}
