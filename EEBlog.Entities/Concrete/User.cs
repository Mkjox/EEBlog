using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using EEBlog.Shared.Entities.Abstract;

namespace EEBlog.Entities.Concrete
{
    public class User : IdentityUser<int>
    {
        // There should be a thing as IdentityUser in this file but it didnt work

        public string Picture { get; set; }
        public ICollection<Post> Posts { get; set; }
        public string About { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string YoutubeLink { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }
        public string LinkedInLink { get; set; }
        public string GithubLink { get; set; }
        public string WebsiteLink { get; set; }
    }
}
