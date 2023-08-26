using EEBlog.Data.Concrete.EntityFramework.Mappings;
using EEBlog.Entities.Concrete;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Data.Concrete.EntityFramework.Contexts
{
    public class EEBlogContext
        : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        //public DbSet<Log> Logs { get; set; }

        //lacks options and base
        public EEBlogContext(DbContextOptions<EEBlogContext>) : base(options)
        {
        }

        //Don't forget to add override
        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            // Don't forget to add others
        }
    }
}
