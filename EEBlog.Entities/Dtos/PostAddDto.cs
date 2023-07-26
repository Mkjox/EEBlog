using EEBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Entities.Dtos
{
    public class PostAddDto
    {
        [DisplayName("Title")]
        [Required(ErrorMessage = "You can't leave {0} blank.")]
        [MaxLength(100, ErrorMessage = "{0} field can't be bigger than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden az olmamalıdır.")]
        public string Title { get; set; }
        [DisplayName("Content")]
        [Required(ErrorMessage = "You can't leave {0} blank.")]
        [MinLength(20, ErrorMessage = "{0} field can't be less than {1} characters.")]
        public string Content { get; set; }
        [DisplayName("Thumbnail")]
        [Required(ErrorMessage = "You can't leave {0} blank.")]
        [MaxLength(250, ErrorMessage = "{0} field can't be bigger than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} field can't be less than {1} characters.")]
        public string Thumbnail { get; set; }
        [DisplayName("Date")]
        [Required(ErrorMessage = "You can't leave {0} blank.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [DisplayName("Seo Writer")]
        [Required(ErrorMessage = "You can't leave {0} blank.")]
        [MaxLength(50, ErrorMessage = "{0} field can't be bigger than {1} characters.")]
        [MinLength(0, ErrorMessage = "{0} field can't be less than {1} characters.")]
        public string SeoAuthor { get; set; }
        [DisplayName("Seo Description")]
        [Required(ErrorMessage = "You can't leave {0} blank.")]
        [MaxLength(150, ErrorMessage = "{0} field can't be bigger than {1} characters.")]
        [MinLength(0, ErrorMessage = "{0} field can't be less than {1} characters.")]
        public string SeoDescription { get; set; }
        [DisplayName("Seo Tags")]
        [Required(ErrorMessage = "You can't leave {0} blank.")]
        [MaxLength(70, ErrorMessage = "{0} field can't be bigger than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} field can't be less than {1} characters.")]
        public string SeoTags { get; set; }
        [DisplayName("Category")]
        [Required(ErrorMessage = "You can't leave {0} blank.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [DisplayName("Is Active?")]
        [Required(ErrorMessage = "You can't leave {0} blank.")]
        public bool IsActive { get; set; }
    }
}
