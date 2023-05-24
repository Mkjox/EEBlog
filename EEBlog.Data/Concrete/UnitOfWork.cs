using EEBlog.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        //private readonly EEBlogContext _context;
        //private EfArticleRepository _articleRepository;
        //private EFCategoryRepository

        public IArticleRepository Articles => throw new NotImplementedException();

        public ICategoryRepository Categories => throw new NotImplementedException();

        public ICommentRepository Comments => throw new NotImplementedException();

        public IRoleRepository Roles => throw new NotImplementedException();

        public IUserRepository Users => throw new NotImplementedException();

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
