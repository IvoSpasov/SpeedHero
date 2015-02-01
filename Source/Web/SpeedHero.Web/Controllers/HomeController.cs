namespace SpeedHero.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using SpeedHero.Data.Common.Repository;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private const int CacheInMinutes = 1;
        private IRepository<Post> posts;

        // This is no logner needed due to Ninject
        // Poor man's dependency injection 
        // The MVC Needs empty constructor to work
        // public HomeController()
        //     : this(new GenericRepository<Post>(new SpeedHeroDbContext()))
        // {
        // }

        // This constructor can be used for unit testing (for instance)
        public HomeController(IRepository<Post> posts)
        {
            this.posts = posts;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [OutputCache(Duration = CacheInMinutes * 60)]
        [ChildActionOnly]
        public ActionResult ShowLatestPosts()
        {
            if (this.posts == null)
            {
                return this.Content("no postst in database");
            }

            var posts = this.posts
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .Take(12)
                .Project()
                .To<IndexViewModel>();

            return this.PartialView("_ShowLatestPosts", posts);
        }

        public ActionResult About()
        {
            return this.View();
        }
    }
}