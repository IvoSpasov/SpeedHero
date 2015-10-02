namespace SpeedHero.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using SpeedHero.Data.Common.Repositories;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.Controllers.Base;
    using SpeedHero.Web.Areas.Administration.ViewModels.Posts;
    using System;

    public class PostController : AdminController
    {
        private readonly IGenericRepository<Post> posts;

        public PostController(IGenericRepository<Post> posts)
        {
            this.posts = posts;
        }

        public ActionResult Index()
        {
            return this.View();
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
            // invalid date from input post so I add it manually
            inputPost.CreatedOn = DateTime.Now;


            if (ModelState.IsValid && inputPost != null)
            {
                var dbPostModel = Mapper.Map<Post>(inputPost);
                this.posts.Add(dbPostModel);
                this.posts.SaveChanges();
                inputPost.Id = dbPostModel.Id;
            }

            return this.Json(new[] { inputPost }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, PostViewModel inputPost)
        {
            if (inputPost != null) //ModelState.IsValid && 
            {
                var post = this.posts.GetById(inputPost.Id);

                //Mapper.Map(inputPost, post);
                post.Title = inputPost.Title;
                post.Content = inputPost.Content;
                this.posts.SaveChanges();
            }

            return this.Json(new[] { inputPost }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, PostViewModel inputPost)
        {
            if (inputPost != null) //ModelState.IsValid && 
            {
                this.posts.Delete(inputPost.Id);
                this.posts.SaveChanges();
            }

            return this.Json(new[] { inputPost }.ToDataSourceResult(request, this.ModelState));
        }
    }
}