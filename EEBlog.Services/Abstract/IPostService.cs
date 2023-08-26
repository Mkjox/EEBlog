using EEBlog.Entities.ComplexTypes;
using EEBlog.Entities.Dtos;
using EEBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Services.Abstract
{
    public interface IPostService
    {
        Task<IDataResult<PostDto>> GetAsync(int postId);
        Task<IDataResult<PostDto>> GetByIdAsync(int postId, bool includeCategory, bool includeComments, bool includeUser);
        Task<IDataResult<PostUpdateDto>> GetPostUpdateDtoAsync(int postId);
        Task<IDataResult<PostListDto>> GetAllAsyncV2(int? categoryId, int? userId, bool? isActive, bool? isDeleted, int currentPage, int pageSize, OrderByGeneral orderBy, bool isAscending, bool includeCategory, bool includeComments, bool includeUser);
        Task<IDataResult<PostListDto>> GetAllAsync();
        Task<IDataResult<PostListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<PostListDto>> GetAllByNonDeletedAndActiveAsync();
        Task<IDataResult<PostListDto>> GetAllByCategoryAsync(int categoryId);
        Task<IDataResult<PostListDto>> GetAllByDeletedAsync();
        Task<IDataResult<PostListDto>> GetAllByViewCountAsync(bool isAscending, int? takeSize);
        Task<IDataResult<PostListDto>> GetAllByPagingAsync(int? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false);
        Task<IDataResult<PostListDto>> GetAllByUserIdOnFilter(int userId, FilterBy filterBy, OrderByGeneral orderBy, bool isAscending, int takeSize, int categoryId, DateTime startAt, DateTime endAt, int minViewCount, int maxViewCount, int minCommentCount, int maxCommentCount);
        Task<IDataResult<PostListDto>> SearchAsync(string keyword, int currentPage = 1, int pageSize = 5, bool isAscending = false);
        Task<IResult> IncreaseViewCountAsync(int postId);
        Task<IResult> AddAsync(PostAddDto postAddDto, string createdByName, int userId);
        Task<IResult> UpdateAsync(PostUpdateDto postUpdateDto, string modifiedByName);
        Task<IDataResult<PostDto>> DeleteAsync(int postId, string modifiedByName);
        Task<IResult> HardDeleteAsync(int postId);
        Task<IDataResult<PostDto>> UndoDeleteAsync(int postId, string modifiedByName);
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();

    }
}
