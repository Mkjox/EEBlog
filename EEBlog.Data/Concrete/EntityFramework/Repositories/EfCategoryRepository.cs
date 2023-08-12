using EEBlog.Data.Abstract;
using EEBlog.Data.Concrete.EntityFramework.Contexts;
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
    public class EfCategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
    {
        public EfCategoryRepository(DbContext context) : base(context)
        {
        }

        public async Task<Category> GetById(int categoryId)
        {
            return await EEBlogContext.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);
        }
    }
}
