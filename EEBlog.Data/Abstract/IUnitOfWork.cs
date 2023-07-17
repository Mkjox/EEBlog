using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Data.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IPostRepository PostRepository { get; }
        ICommentRepository CommentRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IRoleRepository RoleRepository { get; }

        Task<int> SaveAsync();
    }
}
