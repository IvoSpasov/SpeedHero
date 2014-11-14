namespace SpeedHero.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using SpeedHero.Data.Common.Repository;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.InputModels.Comments;
    using System.Web;
    using System.Web.Mvc;

    public class CommentController : Controller
    {
        private IRepository<Comment> comments;

        public CommentController(IRepository<Comment> comments)
        {
            this.comments = comments;
        }

        // TODO Do it with AJAX!!!!!!!!!!!!!!
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
                return RedirectToAction("ShowPost", "Post", new { id = inputComment.PostId });
            }

            return View(inputComment);
        }
    }
}