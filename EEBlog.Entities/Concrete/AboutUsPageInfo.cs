using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Entities.Concrete
{
    public class AboutUsPageInfo
    {
        [DisplayName("Title")]
        [Required(ErrorMessage = "{0} area can't be empty.")]
        [MaxLength(150, ErrorMessage = "{0} area can't be higher than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} area can't be smaller than {1} characters.")]
        public string Header { get; set; }

        [DisplayName("Content")]
        [Required(ErrorMessage = "{0} area can't be empty.")]
        [MaxLength(1500, ErrorMessage = "{0} area can't be bigger than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} area can't be smaller than {1} characters.")]
        public string Content { get; set; }

        [DisplayName("Seo Description")]
        [Required(ErrorMessage = "{0} area can't be empty.")]
        [MaxLength(100, ErrorMessage = "{0} area can't be bigger than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} area can't be smaller than {1} characters.")]
        public string SeoDescription { get; set; }

        [DisplayName("Seo Tags")]
        [Required(ErrorMessage = "{0} are can't be empty.")]
        [MaxLength(100, ErrorMessage = "{0} area can't be bigger than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} area can't be smaller than {1} characters.")]
        public string SeoTags { get; set; }

        [DisplayName("Seo Writer")]
        [Required(ErrorMessage = "{0} area can't be empty.")]
        [MaxLength(60, ErrorMessage = "{0} area can't be bigger than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} area can't be smaller than {1} characters.")]
        public string SeoAuthor {  get; set; }
    }
}
