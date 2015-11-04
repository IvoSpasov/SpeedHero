namespace SpeedHero.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using SpeedHero.Data.Common.Repositories;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private const int CacheInMinutes = 1;
        private readonly IDeletableEntityRepository<Post> postsRepository;
        
        public HomeController(IDeletableEntityRepository<Post> postsDeletableRepository)
        {
            this.postsRepository = postsDeletableRepository;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        // [OutputCache(Duration = CacheInMinutes * 60)]
        [ChildActionOnly]
        public ActionResult ShowLatestPosts()
        {
            var posts = this.postsRepository
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .Take(12)
                .ProjectTo<IndexViewModel>();

            return this.PartialView("_ShowLatestPosts", posts);
        }
    }
}