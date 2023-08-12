using AutoMapper;
using EEBlog.Data.Abstract;
using EEBlog.Entities.Concrete;
using EEBlog.Entities.Dtos;
using EEBlog.Services.Abstract;
using EEBlog.Shared.Utilities.Results.Abstract;
using EEBlog.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Services.Concrete
{
    public class PostManager : ManagerBase, IPostService
    {
        private readonly UserManager<User> _userManager;

        public PostManager(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager) : base(unitOfWork, mapper)
        {
            _userManager = userManager;
        }

        public async Task<IResult> AddAsync(PostAddDto postAddDto, string createdByName, int userId)
        {
            var post = Mapper.Map<Post>(postAddDto);
            post.CreatedByName = createdByName;
            post.ModifiedByName = createdByName;
            post.UserId = userId;
            await UnitOfWork.Posts.AddAsync(post);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.PostMessage.Add(post.Title));
        }

        public async Task<IDataResult<PostDto>> DeleteAsync(int articleId, string modifiedByName)
        {
            var post = await UnitOfWork.Post
        }
    }
}
