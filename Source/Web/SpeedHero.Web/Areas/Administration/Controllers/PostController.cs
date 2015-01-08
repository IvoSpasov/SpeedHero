namespace SpeedHero.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using SpeedHero.Data.Common.Repository;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.Controllers.Base;
    using SpeedHero.Web.Areas.Administration.ViewModels.Posts;

    public class PostController : AdminController
    {
        private readonly IRepository<Post> posts;

        public PostController(IRepository<Post> posts)
        {
            this.posts = posts;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost] // kendo sends data for filtering, paging, sorting
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var posts = this.posts
                .All()
                .Project()
                .To<PostViewModel>();

            DataSourceResult result = posts.ToDataSourceResult(request);

            return this.Json(result);
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, PostViewModel inputPost)
        {            
            if (ModelState.IsValid && inputPost != null)
            {
                var dbPostModel = Mapper.Map<Post>(inputPost);
                this.posts.Add(dbPostModel);
                this.posts.SaveChanges();
                inputPost.Id = dbPostModel.Id;
            }

            return Json(new[] { inputPost }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, PostViewModel inputPost)
        {
            if (ModelState.IsValid && inputPost != null)
            {
                var post = this.posts.GetById(inputPost.Id); // . Value if nullable
                Mapper.Map(inputPost, post);
                this.posts.SaveChanges();
            }

            return Json(new[] { inputPost }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, PostViewModel inputPost)
        {
            if (ModelState.IsValid && inputPost != null)
            {
                this.posts.Delete(inputPost.Id);
                this.posts.SaveChanges();
            }

            return Json(new[] { inputPost }.ToDataSourceResult(request, ModelState));
        }
    }
}