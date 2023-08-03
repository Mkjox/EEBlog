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
                if (isPlural) return "Couldn't find any categories.";
                return "Can't find this category.";
            }

            public static string NotFoundById(int categoryId)
            {
                return $"Couldn't find the category that has {categoryId} id. ";
            }

            public static string Add(string categoryName)
            {
                return $"Successfully added the category named as {categoryName}.";
            }
            
            public static string Update(string categoryName)
            {
                return $"Successfully updated the category named as {categoryName}.";
            }

            public static string Delete(string categoryName)
            {
                return $"Successfully deleted the category named as {categoryName}.";
            }

            public static string HardDelete(string categoryName)
            {
                return $"Successfully deleted the category named as {categoryName} from the database.";
            }

            public static string UndoDelete(string categoryName)
            {
                return $"Successfully retrieved back the category named as {categoryName}.";
            }
        }

        public static class PostMessage
        {
            public static string NotFound(bool isPlural)
            {
                if (!isPlural) return $"Couldn't find the posts.";
                return $"Couldn't find the post.";
            }

            public static string NotFoundById(int postId)
            {
                return $"Couldn't find the post with {postId} id.";
            }

            public static string Add(string postTitle)
            {
                return $"Successfully added the post named as {postTitle}.";
            }

            public static string Update(string postTitle)
            {
                return $"Successfully updated the post named as {postTitle}.";
            }

            public static string Delete(string postTitle)
            {
                return $"Successfully deleted the post named as {postTitle}.";
            }

            public static string HardDelete(string postTitle)
            {
                return $"Successfully deleted the post named as {postTitle} from the database.";
            }

            public static string UndoDelete(string postTitle)
            {
                return $"Successfully retrieved the post back named as {postTitle}.";
            }

            public static string IncreaseViewCount(string postTitle)
            {
                return $"Successfully increased the view count titled as {postTitle}";
            }
        }

        public static class CommentMessage
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return $"Couldn't find any comments.";
                 return $"Couldn't find the comment.";
            }

            public static string NotFoundById(int commentId)
            {
                return $"Couldn't find the comment with the {commentId} id.";
            }

            public static string Add()
            {
                return $"Successfully added the comment.";
            }

            public static string Update()
            {
                return $"Successfully updated the comment.";
            }

            public static string Delete()
            {
                return $"Successfully deleted the comment.";
            }

            public static string HardDelete()
            {
                return $"Successfully deleted the comment from the database.";
            }

            public static string UndoDelete()
            {
                return $"Successfully retrieved the comment from the database.";
            }
        }

        public static class UserMessage
        {
            public static string NotFoundById(int userId)
            {
                return $"Couldn't find the user with {userId} id.";
            }
        }
    }
}
