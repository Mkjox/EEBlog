using EEBlog.Data.Abstract;
using EEBlog.Data.Concrete.EntityFramework.Contexts;
using EEBlog.Data.Concrete.EntityFramework.Repositories;
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
        private EfPostRepository _postRepository;
        private EfCategoryRepository _categoryRepository;
        private EfCommentRepository _commentRepository;

        public UnitOfWork(EEBlogContext context)
        {
            _context = context;
        }

        public IPostRepository Posts => _postRepository ??= new EfPostRepository(_context);

        public ICategoryRepository Categories => _categoryRepository ??= new EfCategoryRepository(_context);

        public ICommentRepository Comments => _commentRepository ??= new EfCommentRepository(_context);


        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
