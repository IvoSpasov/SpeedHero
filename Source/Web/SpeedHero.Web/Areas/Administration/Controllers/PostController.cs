namespace SpeedHero.Web.Areas.Administration.Controllers
{
    using System;
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

    public class PostController : AdminController
    {
        private readonly IGenericRepository<Post> postsRepository;

        public PostController(IDeletableEntityRepository<Post> postsDeletableRepository)
        {
            this.postsRepository = postsDeletableRepository;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost] // kendo sends data for filtering, paging, sorting
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var posts = this.postsRepository
                .All()
                .Project()
                .To<ShowPostsViewModel>();

            DataSourceResult result = posts.ToDataSourceResult(request);

            return this.Json(result);
        }

        //[HttpPost]
        //public ActionResult Create([DataSourceRequest]DataSourceRequest request, PostViewModel inputPost)
        //{
        //    // invalid date from input post so I add it manually
        //    inputPost.CreatedOn = DateTime.Now;

        //    if (ModelState.IsValid && inputPost != null)
        //    {
        //        var dbPostModel = Mapper.Map<Post>(inputPost);
        //        this.postsRepository.Add(dbPostModel);
        //        this.postsRepository.SaveChanges();
        //        inputPost.Id = dbPostModel.Id;
        //    }

        //    return this.Json(new[] { inputPost }.ToDataSourceResult(request, this.ModelState));
        //}

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, UpdatePostViewModel inputPost)
        {
            Post postFromDatabase = null;

            if (ModelState.IsValid)
            {
                postFromDatabase = this.postsRepository.GetById(inputPost.Id);
                Mapper.CreateMap<UpdatePostViewModel, Post>();                                 
                Mapper.Map(inputPost, postFromDatabase);

                //this.postsRepository.Update(postInDatabase);

                this.postsRepository.SaveChanges();                
            }

            var modifedPostForKendo = Mapper.Map<ShowPostsViewModel>(postFromDatabase);

            return this.Json(new[] { modifedPostForKendo }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ShowPostsViewModel inputPost)
        {
            if (inputPost != null) //ModelState.IsValid && 
            {
                this.postsRepository.Delete(inputPost.Id);
                this.postsRepository.SaveChanges();
            }

            return this.Json(new[] { inputPost }.ToDataSourceResult(request, this.ModelState));
        }
    }
}