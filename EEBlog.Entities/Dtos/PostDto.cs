using EEBlog.Entities.Concrete;
using EEBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Entities.Dtos
{
    public class PostDto : DtoGetBase
    {
        public Post Post { get; set; }
    }
}
