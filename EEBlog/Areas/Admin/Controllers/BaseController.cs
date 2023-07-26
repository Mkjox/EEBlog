using AutoMapper;
using EEBlog.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EEBlog.Mvc.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // Don't forget to add IImageHelper
        public BaseController(UserManager<User> userManager, IMapper mapper)
        {
            UserManager = userManager;
            Mapper = mapper;
        }

        protected UserManager<User> UserManager { get; }
        protected IMapper Mapper { get; }
        // Don't forget to add GetUserAsync
    }
}
