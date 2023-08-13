using EEBlog.Services.Abstract;
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
        public async Task<IActionResult> Search(string keyword, int currentPage = 1, int pageSize = 5, bool isAscending)
        {
            var searchResult = await _postService.SearchAsync
        }
    }
}
