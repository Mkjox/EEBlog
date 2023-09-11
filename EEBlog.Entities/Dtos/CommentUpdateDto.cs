using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Entities.Dtos
{
    public class CommentUpdateDto
    {
        [Required(ErrorMessage = "{0} can't be empty.")]
        public int Id { get; set; }

        [DisplayName("Comment")]
        [Required(ErrorMessage = "{0} can't be empty.")]
        [MaxLength(1000, ErrorMessage = "{0} can't be more than {1} characters.")]
        [MinLength(2, ErrorMessage = "{0} can't be less than {1} characters.")]
        public string Text { get; set; }

        [DisplayName("Is it Active?")]
        [Required(ErrorMessage = "{0} can't be empty.")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "{0} can't be empty.")]
        public int PostId { get; set; }
    }
}
