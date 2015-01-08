namespace SpeedHero.Web.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNet.Identity;

    using SpeedHero.Data.Common.Repository;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.InputModels.Comments;
    using SpeedHero.Web.ViewModels.Comments;

    public class CommentController : Controller
    {
        private readonly IRepository<Comment> comments;

        public CommentController(IRepository<Comment> comments)
        {
            this.comments = comments;
        }

        [HttpGet]
        public ActionResult CreateComment(int postId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var commentModel = new CreateCommentInputModel { PostId = postId };
                return this.PartialView("_CreateCommentPartialView", commentModel);
            }

            return this.PartialView("_CommentsLoginPartialView");
        }

        [HttpPost]
        public ActionResult CreateComment(CreateCommentInputModel inputComment)
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

                return this.RedirectToAction("ShowComments", new { postId = inputComment.PostId });
            }

            return this.View(inputComment); // error -> no such view.. fix it!
        }

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

            if (commentsForCurrentPost.Count != 0)
            {
                return this.PartialView("_ShowCommentsPartialView", commentsForCurrentPost);
            }

            return this.PartialView("_NoCommentsPartialView");
        }
    }
}