using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Entities.Dtos
{
    public class CategoryAddDto
    {
        [DisplayName("Category Name")]
        [Required(ErrorMessage = "{0} can't be empty.")]
        [MaxLength(70, ErrorMessage = "{0} can't be more than {1} characters.")]
        [MinLength(3, ErrorMessage = "{0} can't be less than {1} characters.")]
        public string Name { get; set; }

        [DisplayName("Category Description")]
        [MaxLength(500, ErrorMessage = "{0} can't be more than {1} characters.")]
        [MinLength(3, ErrorMessage = "{0} can't be less than {1} characters.")]
        public string Description { get; set; }

        [DisplayName("Category Special Note")]
        [MaxLength(500, ErrorMessage = "{0} can't be more than {1} characters.")]
        [MinLength(3, ErrorMessage = "{0} can't be less than {1} characters.")]
        public string Note { get; set; }

        [DisplayName("Is it Active?")]
        [Required(ErrorMessage = "{0} can't be empty.")]
        public bool IsActive { get; set; }
    }
}
