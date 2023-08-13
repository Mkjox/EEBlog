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
    internal class PostUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "{0} area can't be empty.")]
        [MaxLength(100, ErrorMessage = "{0} area can't be bigger than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} area can't be less than {1} characters.")]
        public string Title { get; set; }

        [DisplayName("Content")]
        [Required(ErrorMessage = "{0} area can't be empty.")]
        [MinLength(20, ErrorMessage = "{0} area can't be less than {1} characters.")]
        public string Content { get; set; }

        [DisplayName("Thumbnail")]
        [Required(ErrorMessage = "{0} area can't be empty.")]
        [MaxLength(250, ErrorMessage = "{0} area can't be less than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} area can't be less than {1} characters.")]
        public string Thumbnail { get; set; }

        [DisplayName("Date")]
        [Required(ErrorMessage = "{0} area can't be empty.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DateFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Seo Author")]
        [Required(ErrorMessage = "{0} area can't be empty.")]
        [MaxLength(150, ErrorMessage = "{0} area can't be bigger than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} can't be less than {1} characters.")]
        public string SeoAuthor { get; set; }

        [DisplayName("Seo Description")]
        [Required(ErrorMessage = "{0} area can't be empty.")]
        [MaxLength(150, ErrorMessage = "{0} area can't be bigger than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} area cant be less than {1} characters.")]
        public string SeoDescription { get; set;}

        [DisplayName("Seo Tags")]
        [Required(ErrorMessage = "{0} area can't be empty.")]
        [MaxLength(70, ErrorMessage = "{0} area can't be bigger than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} area can't be less than {1} characters.")]
        public string SeoTags { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "{0} area can't be empty.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [DisplayName("Is it Active?")]
        [Required(ErrorMessage = "{0} area can't be empty.")]
        public bool IsActive { get; set; }

        [DisplayName("Do you want to Delete?")]
        [Required(ErrorMessage = "{0} area can't be empty.")]
        public bool IsDeleted { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
