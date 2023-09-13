using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Entities.Dtos
{
    public class UserPasswordChangeDto
    {
        [DisplayName("Current Password")]
        [Required(ErrorMessage = "{0} can't be empty.")]
        [MaxLength(30, ErrorMessage = "{0} can't be more than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} can't be less than {1} characters.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DisplayName("New Password")]
        [Required(ErrorMessage = "{0} can't be empty.")]
        [MaxLength(30, ErrorMessage = "{0} can't be more than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} can't be less than {1} characters.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Repeat Your New Password")]
        [Required(ErrorMessage = "{0} can't be empty.")]
        [MaxLength(30, ErrorMessage = "{0} can't be more than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} can't be less than {1} characters.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "New Password and Repeated New Password must match.")]
        public string RepeatPassword { get; set; }
    }
}
