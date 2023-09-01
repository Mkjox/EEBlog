using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Entities.Dtos
{
    public class EmailSendDto
    {
        [DisplayName("Name")]
        [Required(ErrorMessage = "{0} area is mandatory.")]
        [MaxLength(60, ErrorMessage = "{0} area must consist maximum {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} area must consist minimum {1} characters.")]
        public string Name { get; set; }

        [DisplayName("Your E-Mail Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} area is mandatory.")]
        [MaxLength(345, ErrorMessage = "{0} area must consist maximum {1} characters.")]
        [MinLength(10, ErrorMessage = "{0} area must consist minimum {1} characters.")]
        public string Email {  get; set; }

        [DisplayName("Topic")]
        [Required(ErrorMessage = "{0} area is mandatory.")]
        [MaxLength(125, ErrorMessage = "{0} area must consist maximum {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} area must consist minimum {1} characters.")]
        public string Topic { get; set; }

        [DisplayName("Your Message")]
        [Required(ErrorMessage = "{0} area is mandatory.")]
        [MaxLength(1500, ErrorMessage = "{0} area must consist maximum {1} characters.")]
        [MinLength(10, ErrorMessage = "{0} area must consist minimum {1} characters.")]
        public string Message { get; set; }
    }
}
