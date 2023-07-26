using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Services.Utilities
{
    public static class Messages
    {
        public static class General
        {
            public static string ValidationError()
            {
                return $"Encountered one or more validation error.";
            }
        }

        // Message.CategoryMessage.NotFound()
        public static class CategoryMessage
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Couldn't find any categories";
                return "Can't find this category";
            }
        }
    }
}
