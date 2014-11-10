namespace SpeedHero.Web.Controllers
{
    using SpeedHero.Data;
    using SpeedHero.Data.Common.Repository;
    using SpeedHero.Data.Models;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        private IRepository<Post> posts;

        // This is no logner needed due to Ninject
        // Poor man's dependency injection 
        // The MVC Needs empty constructor to work
        //public HomeController()
        //    : this(new GenericRepository<Post>(new SpeedHeroDbContext()))
        //{
        //}

        // This constructor can be used for unit testing (for instance)
        public HomeController(IRepository<Post> posts)
        {
            this.posts = posts;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}