namespace SpeedHero.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using SpeedHero.Data.Common.Repositories;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private const int CacheInMinutes = 1;
        private readonly IGenericRepository<Post> postsRepository;

        // This is no logner needed due to Ninject
        // Poor man's dependency injection
        // public HomeController()
        //     : this(new GenericRepository<Post>(new SpeedHeroDbContext()))
        // {
        // }

        // This constructor can be used for unit testing
        public HomeController(IDeletableEntityRepository<Post> postsDeletableRepository)
        {
            this.postsRepository = postsDeletableRepository;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        //[OutputCache(Duration = CacheInMinutes * 60)]
        [ChildActionOnly]
        public ActionResult ShowLatestPosts()
        {
            Mapper.CreateMap<Post, IndexViewModel>()
                .ForMember(dto => dto.NumberOfComments, opt => opt.MapFrom(p => p.Comments.Count()));

            var posts = this.postsRepository
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .Take(12)
                .Project()
                .To<IndexViewModel>();

            return this.PartialView("_ShowLatestPosts", posts);
        }
    }
}