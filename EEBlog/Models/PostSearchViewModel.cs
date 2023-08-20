using EEBlog.Entities.Dtos;

namespace EEBlog.Mvc.Models
{
    public class PostSearchViewModel
    {
        public PostListDto PostListDto { get; set; }
        public string Keyword { get; set; }
    }
}
