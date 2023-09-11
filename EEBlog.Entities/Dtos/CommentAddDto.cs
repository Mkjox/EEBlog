using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Entities.Dtos
{
    public class CommentAddDto
    {
        [DisplayName("Comment")]
        [Required(ErrorMessage = "{0} can't be empty.")]
        [MaxLength(1000, ErrorMessage = "{0} can't be more than {1} characters.")]
        [MinLength(2, ErrorMessage = "{0} can't be less than {1} characters.")]
        public string Text { get; set; }

        [DisplayName("Your Name")]
        [Required(ErrorMessage = "{0} can't be empty.")]
        [MaxLength(50, ErrorMessage = "{0} can't be more than {1} characters.")]
        [MinLength(2, ErrorMessage = "{0} can't be less than {1} characters.")]
        public string CreatedByName { get; set; }

        [Required(ErrorMessage = "{0} can't be empty.")]
        public int ArticleId { get; set; }
    }
}
