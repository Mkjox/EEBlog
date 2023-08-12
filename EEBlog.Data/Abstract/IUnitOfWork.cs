using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Data.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IPostRepository Posts { get; }
        ICommentRepository Comments { get; }
        ICategoryRepository Categories { get; }
        //IRoleRepository Roles { get; }

        Task<int> SaveAsync();
    }
}
