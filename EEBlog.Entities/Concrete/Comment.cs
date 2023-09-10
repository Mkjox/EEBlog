using EEBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Entities.Concrete
{
    public class Comment : EntityBase, IEntity
    {
        public string Text { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
