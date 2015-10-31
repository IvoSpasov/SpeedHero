namespace SpeedHero.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;

    using SpeedHero.Data.Common.Repositories;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.ViewModels.Comments;

    public class CommentController : Controller
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentController(IDeletableEntityRepository<Comment> commentsDeletableRepository)
        {
            this.commentsRepository = commentsDeletableRepository;
        }

        // [ChildActionOnly] It does not work with RedirectToAction.
        [AllowAnonymous]
        [HttpGet]
        public ActionResult CreateComment(int postId)
        {
            if (User.Identity.IsAuthenticated)
            {
                return this.PartialView("_CreateCommentPartialView", postId);
            }

            return this.PartialView("_CommentsLoginPartialView", postId);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(CreateCommentViewModel inputComment)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<CreateCommentViewModel, Comment>();
                var comment = Mapper.Map<Comment>(inputComment);
                comment.AuthorId = this.User.Identity.GetUserId();
                this.commentsRepository.Add(comment);
                this.commentsRepository.SaveChanges();
            }
            else
            {
                this.TempData["invalidComment"] = "No text in the comment field.";
            }

            return this.RedirectToAction("CreateComment", new { postId = inputComment.PostId });
        }

        [AllowAnonymous]
        [ChildActionOnly]
        [HttpGet]
        public ActionResult ShowComments(int postId)
        {
            var commentsForCurrentPost = this.commentsRepository
                .All()
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedOn)
                .ProjectTo<ShowCommentViewModel>();

            return this.PartialView("_ShowCommentsPartialView", commentsForCurrentPost);
        }
    }
}