using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Entities.Dtos
{
    public class ArticleAddDto
    {
        [DisplayName("Title")]
        [Required(ErrorMessage = "{0} field can't be bigger than {1} characters.")]
        [MaxLength(100, ErrorMessage = "{0} field can't be bigger than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} field can't be smaller than {1} characters.")]
        public string Title { get; set; }


    }
}
