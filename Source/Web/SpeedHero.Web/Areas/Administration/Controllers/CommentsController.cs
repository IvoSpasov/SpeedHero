namespace SpeedHero.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using SpeedHero.Data.Common.Repositories;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.Controllers.Base;
    using SpeedHero.Web.Areas.Administration.ViewModels.Comments;

    public class CommentsController : AdminController
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsController(IDeletableEntityRepository<Comment> commentsDeletableRepository)
        {
            this.commentsRepository = commentsDeletableRepository;
        }

        // GET: Administration/Comments
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ShowAllInPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comments = this.commentsRepository
                .All()
                .Where(c => c.PostId == id.Value)
                .ProjectTo<ShowCommentsViewModel>();

            return this.PartialView("_ShowAllInPost", comments);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var commentFromDatabase = this.commentsRepository
                .GetById(id.Value);

            if (commentFromDatabase == null)
            {
                return this.HttpNotFound("Comment not found");
            }

            var mappedComment = Mapper.Map<EditCommentViewModel>(commentFromDatabase);

            ViewBag.PostId = commentFromDatabase.PostId;
            return this.View(mappedComment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCommentViewModel inputComment)
        {
            if (ModelState.IsValid)
            {
                var commentFromDatabase = this.commentsRepository.GetById(inputComment.Id);
                Mapper.CreateMap<EditCommentViewModel, Comment>();
                Mapper.Map(inputComment, commentFromDatabase);

                this.commentsRepository.Update(commentFromDatabase);
                this.commentsRepository.SaveChanges();

                return this.RedirectToAction("Details", "Posts", new { id = commentFromDatabase.PostId });
            }

            return this.View(inputComment);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var commentFromDatabase = this.commentsRepository
                .GetById(id.Value);

            if (commentFromDatabase == null)
            {
                return this.HttpNotFound("Comment not found");
            }

            this.commentsRepository.Delete(id.Value);
            this.commentsRepository.SaveChanges();

            return this.RedirectToAction("Details", "Posts", new { id = commentFromDatabase.PostId });
        }
    }
}