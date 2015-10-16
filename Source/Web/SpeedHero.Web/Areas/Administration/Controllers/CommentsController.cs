namespace SpeedHero.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using SpeedHero.Data.Common.Repositories;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.ViewModels.Comments;

    public class CommentsController : Controller
    {
        private IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsController(IDeletableEntityRepository<Comment> commentsDeletableRepository)
        {
            this.commentsRepository = commentsDeletableRepository;
        }

        // GET: Administration/Comments
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowComments(int? id)
        {
            var comments = this.commentsRepository
                .All()
                .Where(c => c.PostId == id.Value)
                .Project()
                .To<ShowCommentsViewModel>();

            return PartialView("_ShowComments", comments);
        }
    }
}