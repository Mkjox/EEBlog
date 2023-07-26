using EEBlog.Models;
using EEBlog.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EEBlog.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        [Route("index")]
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId, int pageSize = 5, bool isAscending = false)
        {
            var postResult = await (categoryId ==null
                ? _postService.GetAllByPagingAsync(null, currentPage, pageSize, isAscending)
                : _postService.GetAllByPagingAsync(categoryId.Value, currentPage, pageSize, isAscending));
            return View(postResult.Data);
        }













        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}