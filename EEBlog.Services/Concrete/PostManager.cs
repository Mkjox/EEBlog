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
using Microsoft.EntityFrameworkCore;
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
                if (!await UnitOfWork.Categories.AnyAsync(c => c.Id == categoryId.Value))
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

            if (userId.HasValue)
            {
                if (!await _userManager.Users.AnyAsync(u => u.Id == userId.Value))
                {
                    return new DataResult<PostListDto>(ResultStatus.Warning, Messages.General.ValidationError(), null, new List<ValidationError> {
                        new ValidationError
                        {
                            PropertyName = "userId",
                            Message = Messages.UserMessage.NotFoundById(userId.Value)
                        }
                    });
                }
                predicates.Add(p => p.UserId == userId.Value);
            }
            if (isActive.HasValue) predicates.Add(p => p.IsActive == isActive.Value);
            if (isDeleted.HasValue) predicates.Add(p => p.IsDeleted == isDeleted.Value);
            // includes
            if (includeCategory) includes.Add(p => p.Category);
            if (includeComments) includes.Add(p => p.Comments);
            if (includeUser) includes.Add(p => p.User);
            var posts = await UnitOfWork.Posts.GetAllAsyncV2(predicates, includes);

            IOrderedEnumerable<Post> sortedPosts;

            switch (orderBy)
            {
                case OrderByGeneral.Id:
                    sortedPosts = isAscending ? posts.OrderBy(p => p.Id) : posts.OrderByDescending(p => p.Id);
                    break;
                case OrderByGeneral.Az:
                    sortedPosts = isAscending ? posts.OrderBy(p => p.Title) : posts.OrderByDescending(p => p.Title);
                    break;
                default:
                    sortedPosts = isAscending ? posts.OrderBy(p => p.CreatedDate) : posts.OrderByDescending(p => p.CreatedDate);
                    break;
                    // When it comes to dates, ascending order would mean that the oldest ones come first and the most recent ones last.
            }

            return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto
            {
                Posts = sortedPosts.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(),
                CategoryId = categoryId.HasValue ? categoryId.Value : null,
                CurrentPage = currentPage,
                PageSize = pageSize,
                IsAscending = isAscending,
                TotalCount = posts.Count,
                ResultStatus = ResultStatus.Success,
            });
        }

        public async Task<IDataResult<PostListDto>> GetAllByCategoryAsync(int categoryId)
        {
            var result = await UnitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var posts = await UnitOfWork.Posts.GetAllAsync(p => p.CategoryId == categoryId && !p.IsDeleted && p.IsActive, po => po.User, po => po.Category);
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
            return new DataResult<PostListDto>(ResultStatus.Error, Messages.PostMessage.NotFound(isPlural: false), null);
        }

        public async Task<IDataResult<PostListDto>> GetAllByDeletedAsync()
        {
            var posts = await UnitOfWork.Posts.GetAllAsync(p => p.IsDeleted, po => po.User, po => po.Category); //Fetch deleted ones
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

        public async Task<IDataResult<PostListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            var posts = await UnitOfWork.Posts.GetAllAsync(p => !p.IsDeleted && p.IsActive, po => po.User, po => po.Category); // Fetch active and not deleted posts
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

        public async Task<IDataResult<PostListDto>> GetAllByNonDeletedAsync()
        {
            var posts = await UnitOfWork.Posts.GetAllAsync(p => !p.IsDeleted, po => po.User, po => po.Category);
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

        public async Task<IDataResult<PostListDto>> GetAllByPagingAsync(int? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            var posts = categoryId == null
                ? await UnitOfWork.Posts.GetAllAsync(p => p.IsActive && !p.IsDeleted, p => p.Category, p => p.User)
                : await UnitOfWork.Posts.GetAllAsync(p => p.CategoryId == categoryId && p.IsActive && !p.IsDeleted, p => p.Category, p => p.User);

            var sortedPosts = isAscending
                ? posts.OrderBy(p => p.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : posts.OrderByDescending(p => p.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto
            {
                Posts = sortedPosts,
                CategoryId = categoryId == null ? null : categoryId.Value,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = posts.Count,
                IsAscending = isAscending
            });
        }

        public async Task<IDataResult<PostListDto>> GetAllByUserIdOnFilter(int userId, FilterBy filterBy, OrderBy orderBy, bool isAscending, int takeSize, int categoryId, DateTime startAt, DateTime endAt, int minViewCount, int maxViewCount, int minCommentCount, int maxCommentCount)
        {
            var anyUser = await _userManager.Users.AnyAsync(u => u.Id == userId);
            if (!anyUser)
            {
                return new DataResult<PostListDto>(ResultStatus.Error, $"User with {userId} number couldn't be found.", null);
            }

            var userPosts = await UnitOfWork.Posts.GetAllAsync(p => p.IsActive && !p.IsDeleted && p.UserId == userId);
            List<Post> sortedPosts = new List<Post>();

            switch (filterBy)
            {
                case FilterBy.Category:
                    switch (orderBy)
                    {
                        case OrderBy.Date:
                            sortedPosts = isAscending
                                ? userPosts.Where(p => p.CategoryId == categoryId).Take(takeSize).OrderBy(p => p.CreatedDate).ToList()
                                : userPosts.Where(p => p.CategoryId == categoryId).Take(takeSize).OrderByDescending(p => p.CreatedDate).ToList();
                            break;

                        case OrderBy.ViewCount:
                            sortedPosts = isAscending
                                ? userPosts.Where(p => p.CategoryId == categoryId).Take(takeSize).OrderBy(p => p.ViewCount).ToList()
                                : userPosts.Where(p => p.CategoryId == categoryId).Take(takeSize).OrderByDescending(p => p.ViewCount).ToList();
                            break;

                        case OrderBy.CommentCount:
                            sortedPosts = isAscending
                                ? userPosts.Where(p => p.CategoryId == categoryId).Take(takeSize).OrderBy(p => p.CommentCount).ToList()
                                : userPosts.Where(p => p.CategoryId == categoryId).Take(takeSize).OrderByDescending(p => p.CommentCount).ToList();
                            break;
                    }
                    break;

                case FilterBy.Date:
                    switch (orderBy)
                    {
                        case OrderBy.Date:
                            sortedPosts = isAscending
                                ? userPosts.Where(p => p.Date >= startAt && p.Date <= endAt).Take(takeSize).OrderBy(p => p.CreatedDate).ToList()
                                : userPosts.Where(p => p.Date >= startAt && p.Date <= endAt).OrderByDescending(p => p.CreatedDate).ToList();
                            break;

                        case OrderBy.ViewCount:
                            sortedPosts = isAscending
                                ? userPosts.Where(p => p.Date >= startAt && p.Date <= endAt).OrderBy(p => p.ViewCount).ToList()
                                : userPosts.Where(p => p.Date >= startAt && p.Date <= endAt).OrderByDescending(p => p.ViewCount).ToList();
                            break;

                        case OrderBy.CommentCount:
                            sortedPosts = isAscending
                                ? userPosts.Where(p => p.Date >= startAt && p.Date <= endAt).OrderBy(p => p.CommentCount).ToList()
                                : userPosts.Where(p => p.Date >= startAt && p.Date <= endAt).OrderByDescending(p => p.CommentCount).ToList();
                            break;
                    }
                    break;
                case FilterBy.ViewCount:
                    switch (orderBy)
                    {
                        case OrderBy.Date:
                            sortedPosts = isAscending
                                ? userPosts.Where(p => p.ViewCount >= minViewCount && p.ViewCount <= maxViewCount).Take(takeSize).OrderBy(p => p.CreatedDate).ToList()
                                : userPosts.Where(p => p.ViewCount >= minViewCount && p.ViewCount <= maxViewCount).OrderByDescending(p => p.CreatedDate).ToList();
                            break;

                        case OrderBy.ViewCount:
                            sortedPosts = isAscending
                                ? userPosts.Where(p => p.ViewCount >= minViewCount && p.ViewCount <= maxViewCount).OrderBy(p => p.ViewCount).ToList()
                                : userPosts.Where(p => p.ViewCount >= minViewCount && p.ViewCount <= maxViewCount).OrderByDescending(p => p.ViewCount).ToList();
                            break;

                        case OrderBy.CommentCount:
                            sortedPosts = isAscending
                                ? userPosts.Where(p => p.ViewCount >= minViewCount && p.ViewCount <= maxViewCount).OrderBy(p => p.CommentCount).ToList()
                                : userPosts.Where(p => p.ViewCount >= minViewCount && p.ViewCount <= maxViewCount).OrderByDescending(p => p.CommentCount).ToList();
                            break;
                    }
                    break;
                case FilterBy.CommentCount:
                    switch (orderBy)
                    {
                        case OrderBy.Date:
                            sortedPosts = isAscending
                                ? userPosts.Where(p => p.CommentCount >= minCommentCount && p.CommentCount <= maxCommentCount).Take(takeSize).OrderBy(p => p.CreatedDate).ToList()
                                : userPosts.Where(p => p.CommentCount >= minCommentCount && p.CommentCount <= maxCommentCount).OrderByDescending(p => p.CreatedDate).ToList();
                            break;
                        case OrderBy.ViewCount:
                            sortedPosts = isAscending
                                ? userPosts.Where(p => p.CommentCount >= minCommentCount && p.CommentCount <= maxCommentCount).OrderBy(p => p.ViewCount).ToList()
                                : userPosts.Where(p => p.CommentCount >= minCommentCount && p.CommentCount <= maxCommentCount).OrderByDescending(p => p.ViewCount).ToList();
                            break;
                        case OrderBy.CommentCount:
                            sortedPosts = isAscending
                                ? userPosts.Where(p => p.CommentCount >= minCommentCount && p.CommentCount <= maxCommentCount).OrderBy(p => p.CommentCount).ToList()
                                : userPosts.Where(p => p.CommentCount >= minCommentCount && p.CommentCount <= maxCommentCount).OrderByDescending(p => p.CommentCount).ToList();
                            break;
                    }
                    break;
            }
            return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto
            {
                Posts = sortedPosts
            });
        }

        public async Task<IDataResult<PostListDto>> GetAllByViewCountAsync(bool isAscending, int? takeSize)
        {
            var posts = await UnitOfWork.Posts.GetAllAsync(p => p.IsActive && !p.IsDeleted, p => p.Category, p => p.User);
            var sortedPosts = isAscending ? posts.OrderBy(p => p.ViewCount) : posts.OrderByDescending(p => p.ViewCount);
            return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto
            {
                Posts = takeSize == null ? sortedPosts.ToList() : sortedPosts.Take(takeSize.Value).ToList()
            });
        }

        public async Task<IDataResult<PostDto>> GetAsync(int postId)
        {
            var post = await UnitOfWork.Posts.GetAsync(p => p.Id == postId, p => p.User, p => p.Category);
            if (post != null)
            {
                post.Comments = await UnitOfWork.Comments.GetAllAsync(c => c.PostId == postId && !c.IsDeleted && c.IsActive);
                return new DataResult<PostDto>(ResultStatus.Success, new PostDto
                {
                    Post = post,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<PostDto>(ResultStatus.Error, Messages.PostMessage.NotFound(isPlural: false), null);
        }

        public async Task<IDataResult<PostDto>> GetByIdAsync(int postId, bool includeCategory, bool includeComments, bool includeUser)
        {
            List<Expression<Func<Post, bool>>> predicates = new List<Expression<Func<Post, bool>>>();
            List<Expression<Func<Post, object>>> includes = new List<Expression<Func<Post, object>>>();
            if (includeCategory) includes.Add(p => p.Category);
            if (includeComments) includes.Add(p => p.Comments);
            if (includeUser) includes.Add(p => p.User);
            predicates.Add(p => p.Id == postId);
            var post = await UnitOfWork.Posts.GetAsyncV2(predicates, includes);
            if (post == null)
            {
                return new DataResult<PostDto>(ResultStatus.Warning, Messages.General.ValidationError(), null, new List<ValidationError>
                {
                    new ValidationError
                    {
                        PropertyName="postId",
                        Message = Messages.PostMessage.NotFoundById(postId)
                    }
                });
            }
            return new DataResult<PostDto>(ResultStatus.Success, new PostDto
            {
                Post = post
            });
        }

        public Task<IDataResult<PostUpdateDto>> GetPostUpdateDtoAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> HardDeleteAsync(int postId)
        {
            var result = await UnitOfWork.Posts.AnyAsync(p => p.Id == postId);
            if (result)
            {
                var post = await UnitOfWork.Posts.GetAsync(p => p.Id == postId);
                await UnitOfWork.Posts.DeleteAsync(post);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.PostMessage.HardDelete(post.Title));
            }
            return new Result(ResultStatus.Error, Messages.PostMessage.NotFound(isPlural: false));
        }

        public async Task<IResult> IncreaseViewCountAsync(int postId)
        {
            var post = await UnitOfWork.Posts.GetAsync(p => p.Id == postId);
            if (post == null)
            {
                return new Result(ResultStatus.Error, Messages.PostMessage.NotFound(isPlural: false));
            }
            post.ViewCount += 1;
            await UnitOfWork.Posts.UpdateAsync(post);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.PostMessage.IncreaseViewCount(post.Title));
        }

        public async Task<IDataResult<PostListDto>> SearchAsync(string keyword, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                var posts = await UnitOfWork.Posts.GetAllAsync(p => p.IsActive && !p.IsDeleted, p => p.Category, p => p.User);
                var sortedPosts = isAscending
                    ? posts.OrderBy(p => p.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                    : posts.OrderByDescending(p => p.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto
                {
                    Posts = sortedPosts,
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    TotalCount = posts.Count,
                    IsAscending = isAscending
                });
            }
            var searchedPosts = await UnitOfWork.Posts.SearchAsync(new List<Expression<Func<Post, bool>>>
            {
                (p)=>p.Title.Contains(keyword),
                (p)=>p.Category.Name.Contains(keyword),
                (p)=>p.SeoDescription.Contains(keyword),
                (p)=>p.SeoTags.Contains(keyword)
            }, p => p.Category, p => p.User);

            var searchedAndSortedPosts = isAscending
                ? searchedPosts.OrderBy(p => p.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : searchedPosts.OrderByDescending(p => p.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new DataResult<PostListDto>(ResultStatus.Success, new PostListDto
            {
                Posts = searchedAndSortedPosts,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = searchedPosts.Count,
                IsAscending = isAscending
            });
        }

        public async Task<IDataResult<PostDto>> UndoDeleteAsync(int postId, string modifiedByName)
        {
            var post = await UnitOfWork.Posts.GetAsync(p => p.Id == postId);
            if (post != null)
            {
                post.IsDeleted = false;
                post.IsActive = true;
                post.ModifiedByName = modifiedByName;
                post.ModifiedDate = DateTime.Now;
                var deletedPost = await UnitOfWork.Posts.UpdateAsync(post);
                await UnitOfWork.SaveAsync();
                return new DataResult<PostDto>(ResultStatus.Success, Messages.PostMessage.UndoDelete(post.Title), new PostDto
                {
                    Post = deletedPost,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.PostMessage.UndoDelete(post.Title)
                });
            }
            return new DataResult<PostDto>(ResultStatus.Error, Messages.PostMessage.NotFound(isPlural: false), new PostDto
            {
                Post= null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.PostMessage.NotFound(isPlural: false)
            });
        }

        public async Task<IResult> UpdateAsync(PostUpdateDto postUpdateDto, string modifiedByName)
        {
            var oldPost = await UnitOfWork.Posts.GetAsync(p => p.Id == postUpdateDto.Id);
            var post = Mapper.Map<PostUpdateDto, Post>(postUpdateDto, oldPost);
            post.ModifiedByName = modifiedByName;
            await UnitOfWork.Posts.UpdateAsync(post);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.PostMessage.Update(post.Title));
        }
    }
}
