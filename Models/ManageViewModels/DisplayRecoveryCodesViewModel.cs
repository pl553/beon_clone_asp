using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Beon.Models.ManageViewModels
{
    public class DisplayRecoveryCodesViewModel
    {
        [Required]
        public IEnumerable<string> Codes { get; set; }

    }
}
