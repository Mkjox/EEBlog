using EEBlog.Entities.Concrete;
using EEBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Entities.Dtos
{
    public class PostListDto : DtoGetBase
    {
        public IList<Post> Posts { get; set; }
        public int? CategoryId { get; set; }
    }
}
