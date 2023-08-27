using EEBlog.Entities.Concrete;
using EEBlog.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Data.Abstract
{
    public interface IPostRepository :IEntityRepository<Post>
    {
    }
}
