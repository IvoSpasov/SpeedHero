namespace SpeedHero.Web.Areas.Administration.Controllers
{

    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using SpeedHero.Data.Common.Repository;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.Controllers.Base;
    using System.Web.Mvc;
    using SpeedHero.Web.Areas.Administration.ViewModels.Posts;
    using AutoMapper;

    public class PostController : AdminController
    {
        private IRepository<Post> posts;

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
                .To<PostViewModel>()
                .ToDataSourceResult(request);

            return this.Json(posts);
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, PostViewModel inputPost)
        {
            if (ModelState.IsValid && inputPost != null)
            {
                var dbModel = Mapper.Map<Post>(inputPost);
                this.posts.Add(dbModel);
                this.posts.SaveChanges();
                inputPost.Id = dbModel.Id;
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