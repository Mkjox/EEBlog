using EEBlog.Data.Abstract;
using EEBlog.Entities.Concrete;
using EEBlog.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Data.Concrete.EntityFramework.Repositories
{
    public class EfPostRepository : EfEntityRepositoryBase<Post>, IPostRepository
    {
        public EfPostRepository(DbContext context) : base(context)
        {

        }
    }
}
