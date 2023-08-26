using AutoMapper;
using EEBlog.Data.Abstract;
using EEBlog.Entities.ComplexTypes;
using EEBlog.Entities.Concrete;
using EEBlog.Entities.Dtos;
using EEBlog.Services.Abstract;
using EEBlog.Services.Utilities;
using EEBlog.Shared.Utilities.Results.Abstract;
using EEBlog.Shared.Utilities.Results.ComplexTypes;
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

        public Task<IResult> AddAsync(PostAddDto postAddDto, string createdByName, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<int>> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PostDto>> DeleteAsync(int postId, string modifiedByName)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PostListDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PostListDto>> GetAllAsyncV2(int? categoryId, int? userId, bool? isActive, bool? isDeleted, int currentPage, int pageSize, OrderByGeneral orderBy, bool isAscending, bool includeCategory, bool includeComments, bool includeUser)
        {
            throw new NotImplementedException();
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
