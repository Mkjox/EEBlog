using AutoMapper;
using EEBlog.Data.Abstract;
using EEBlog.Entities.ComplexTypes;
using EEBlog.Entities.Concrete;
using EEBlog.Entities.Dtos;
using EEBlog.Services.Abstract;
using EEBlog.Services.Utilities;
using EEBlog.Shared.Entities.Concrete;
using EEBlog.Shared.Utilities.Results.Abstract;
using EEBlog.Shared.Utilities.Results.ComplexTypes;
using EEBlog.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IDataResult<int>> CountAsync()
        {
            var postCount = await UnitOfWork.Posts.CountAsync();
            if (postCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, postCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"There is an unexpected error.", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var postsCount = await UnitOfWork.Posts.CountAsync(p => !p.IsDeleted);
            if (postsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, postsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"There is an unexpected error.", -1);
            }
        }

        public async Task<IDataResult<PostDto>> DeleteAsync(int postId, string modifiedByName)
        {
            var post = await UnitOfWork.Posts.GetAsync(p => p.Id == postId);
            if (post != null)
            {
                post.IsDeleted = true;
                post.IsActive = false;
                post.ModifiedByName = modifiedByName;
                //post.ModifiedDate = DateTime.Now;
                var deletedPost = await UnitOfWork.Posts.UpdateAsync(post);
                return new DataResult<PostDto>(ResultStatus.Success, Messages.PostMessage.Delete(deletedPost.Title), new PostDto
                {
                    Post = deletedPost,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.PostMessage.Delete(deletedPost.Title)
                });
            }
            return new DataResult<PostDto>(ResultStatus.Error, Messages.PostMessage.NotFound(isPlural: false), new PostDto
            {
                Post = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.PostMessage.NotFound(isPlural: false)
            });
        }

        public async Task<IDataResult<PostListDto>> GetAllAsync()
        {
            var posts = await UnitOfWork.Posts.GetAllAsync(null, p => p.User, p => p.Category);
            if (posts.Count > -1)
            {
                return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto
                {
                    Posts = posts,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<PostListDto>(ResultStatus.Error, Messages.PostMessage.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<PostListDto>> GetAllAsyncV2(int? categoryId, int? userId, bool? isActive, bool? isDeleted, int currentPage, int pageSize, OrderByGeneral orderBy, bool isAscending, bool includeCategory, bool includeComments, bool includeUser)
        {
            List<Expression<Func<Post, bool>>> predicates = new List<Expression<Func<Post, bool>>>();
            List<Expression<Func<Post, object>>> includes = new List<Expression<Func<Post, object>>>();

            // predicates
            if (categoryId.HasValue)
            {
                if(!await UnitOfWork.Categories.AnyAsync(c=>c.Id == categoryId.Value))
                {
                    return new DataResult<PostListDto>(ResultStatus.Warning, Messages.General.ValidationError(), null, new List<ValidationError>
                    {
                        new ValidationError
                        {
                            PropertyName = "categoryId",
                            Message = Messages.CategoryMessage.NotFoundById(categoryId.Value)
                        }
                    });
                }
                predicates.Add(p => p.CategoryId == categoryId.Value);
            }
        }

        public Task<IDataResult<PostListDto>> GetAllByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PostListDto>> GetAllByDeletedAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PostListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PostListDto>> GetAllByNonDeletedAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PostListDto>> GetAllByPagingAsync(int? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PostListDto>> GetAllByUserIdOnFilter(int userId, FilterBy filterBy, OrderByGeneral orderBy, bool isAscending, int takeSize, int categoryId, DateTime startAt, DateTime endAt, int minViewCount, int maxViewCount, int minCommentCount, int maxCommentCount)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PostListDto>> GetAllByViewCountAsync(bool isAscending, int? takeSize)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PostDto>> GetAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PostDto>> GetByIdAsync(int postId, bool includeCategory, bool includeComments, bool includeUser)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PostUpdateDto>> GetPostUpdateDtoAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> HardDeleteAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> IncreaseViewCountAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PostListDto>> SearchAsync(string keyword, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PostDto>> UndoDeleteAsync(int postId, string modifiedByName)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateAsync(PostUpdateDto postUpdateDto, string modifiedByName)
        {
            throw new NotImplementedException();
        }
    }
}
