using EEBlog.Data.Abstract;
using EEBlog.Data.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EEBlogContext _context;
        //private EfArticleRepository _articleRepository;
        //private EFCategoryRepository


        public IUserRepository Users => throw new NotImplementedException();

        public IPostRepository PostRepository => throw new NotImplementedException();

        public ICommentRepository CommentRepository => throw new NotImplementedException();

        public ICategoryRepository CategoryRepository => throw new NotImplementedException();

        public IRoleRepository RoleRepository => throw new NotImplementedException();

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
