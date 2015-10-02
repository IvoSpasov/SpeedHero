namespace SpeedHero.Web.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNet.Identity;

    using SpeedHero.Data.Common.Repositories;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.ViewModels.Comments;
    using SpeedHero.Web.ViewModels.Comments;

    public class CommentController : Controller
    {
        private readonly IGenericRepository<Comment> comments;

        public CommentController(IGenericRepository<Comment> comments)
        {
            this.comments = comments;
        }

        // [ChildActionOnly]
        [HttpGet]
        public ActionResult CreateComment(int postId, bool isValidComment = true)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (isValidComment == false)
                {
                    ViewBag.Comment = "Invalid";
                }

                var commentModel = new CreateCommentViewModel { PostId = postId };
                return this.PartialView("_CreateCommentPartialView", commentModel);
            }

            return this.PartialView("_CommentsLoginPartialView", postId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(CreateCommentViewModel inputComment)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = this.User.Identity.GetUserId();
                string content = HttpUtility.HtmlDecode(inputComment.Content);

                var comment = new Comment
                {
                    Content = content,
                    PostId = inputComment.PostId,
                    AuthorId = currentUserId
                };

                this.comments.Add(comment);
                this.comments.SaveChanges();

                return this.RedirectToAction("CreateComment", new { postId = inputComment.PostId });
            }

            return this.RedirectToAction("CreateComment", new { postId = inputComment.PostId, isValidComment = false });
        }

        // [ChildActionOnly]
        [HttpGet]
        public ActionResult ShowComments(int postId)
        {
            var commentsForCurrentPost = this.comments
                .All()
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedOn)
                .Project()
                .To<ShowCommentViewModel>()
                .ToList();

            if (commentsForCurrentPost.Count == 0)
            {
                return this.PartialView("_NoCommentsPartialView");
            }

            return this.PartialView("_ShowCommentsPartialView", commentsForCurrentPost);
        }
    }
}