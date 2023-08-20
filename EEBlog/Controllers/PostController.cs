using EEBlog.Mvc.Models;
using EEBlog.Services.Abstract;
using EEBlog.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace EEBlog.Mvc.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        //private readonly PostRightSideBarWidgetOptions _postRightSideBarWidgetOptions;

        private PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> Search(string keyword, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            var searchResult = await _postService.SearchAsync(keyword, currentPage, pageSize, isAscending);
            if (searchResult.ResultStatus == ResultStatus.Success)
            {
                return View(new PostSearchViewModel
                {
                    /* Fix searchResult.Data later
                    PostListDto = searchResult.Data,*/
                    Keyword = keyword
                });
            }
            return NotFound();
        }

        /* Add these later
        [HttpGet]
        //[ViewCountFilter]
        public async Task<IActionResult> Detail(int postId)
        {
            var postResult = await _postService.GetAsync(postId);
            if(postResult.ResultStatus == ResultStatus.Success)
            {
                var userPosts = await _postService.GetAllByUserIdOnFilter(postResult.Data.Post.UserId)
            }
        }*/
    }
}
