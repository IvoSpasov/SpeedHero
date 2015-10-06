namespace SpeedHero.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using SpeedHero.Data.Common.RepositoryInterfaces;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.Controllers.Base;
    using SpeedHero.Web.Areas.Administration.ViewModels.Posts;
    using System;

    public class PostController : AdminController
    {
        private readonly IGenericRepository<Post> postsRepository;

        public PostController(IGenericRepository<Post> postsGenericRepository)
        {
            this.postsRepository = postsGenericRepository;
        }

        public ActionResult Index()
        {
            Mapper.CreateMap<Post, PostViewModel>()
                .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(p => p.Author.UserName));


            return this.View();
        }

        [HttpPost] // kendo sends data for filtering, paging, sorting
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var posts = this.postsRepository
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
                this.postsRepository.Add(dbPostModel);
                this.postsRepository.SaveChanges();
                inputPost.Id = dbPostModel.Id;
            }

            return this.Json(new[] { inputPost }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, PostViewModel inputPost)
        {
            if (inputPost != null) //ModelState.IsValid && 
            {
                var post = this.postsRepository.GetById(inputPost.Id);

                //Mapper.Map(inputPost, post);
                post.Title = inputPost.Title;
                post.Content = inputPost.Content;
                this.postsRepository.SaveChanges();
            }

            return this.Json(new[] { inputPost }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, PostViewModel inputPost)
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