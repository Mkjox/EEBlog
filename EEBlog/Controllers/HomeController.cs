using EEBlog.Entities.Concrete;
using EEBlog.Entities.Dtos;
using EEBlog.Models;
using EEBlog.Services.Abstract;
using EEBlog.Shared.Utilities.Helpers.Abstract;
using EEBlog.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EEBlog.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly AboutUsPageInfo _aboutUsPageInfo;
        private readonly IWritableOptions<AboutUsPageInfo> _aboutUsPageInfoWriter;
        private readonly IMailService _mailService;

        public HomeController(IPostService postService, AboutUsPageInfo aboutUsPageInfo, IWritableOptions<AboutUsPageInfo> aboutUsPageInfoWriter, IMailService mailService)
        {
            _postService = postService;
            _aboutUsPageInfo = aboutUsPageInfo;
            _aboutUsPageInfoWriter = aboutUsPageInfoWriter;
            _mailService = mailService;
        }

        [Route("index")]
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            var postResult = await (categoryId == null
                ? _postService.GetAllByPagingAsync(null, currentPage, pageSize, isAscending)
                : _postService.GetAllByPagingAsync(categoryId.Value, currentPage, pageSize, isAscending));
            return View(postResult.Data);
        }

        [Route("aboutUs")]
        [Route("about")]
        [HttpGet]
        public IActionResult About()
        {
            return View(_aboutUsPageInfo);
        }

        [Route("contact")]
        [Route("contactUs")]
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [Route("contact")]
        [Route("contactUs")]
        [HttpPost]
        public IActionResult Contact(EmailSendDto emailSendDto)
        {
            if (ModelState.IsValid)
            {
                var result = _mailService.SendContactEmail(emailSendDto);
                //add ToastNotification here
                return View();
            }
            return View(emailSendDto);
        }

        [Route("random-post")]
        [HttpGet]
        public async Task<IActionResult> GetRandom()
        {
            var result = await _postService.GetAllByNonDeletedAndActiveAsync();
            if (result.ResultStatus == ResultStatus.Success) return View(result.Data);
            return NotFound();
        }
    }
}