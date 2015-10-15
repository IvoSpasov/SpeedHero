namespace SpeedHero.Web.Areas.Administration.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using SpeedHero.Data.Common.Repositories;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.Controllers.Base;
    using SpeedHero.Web.Areas.Administration.ViewModels.Posts;

    public class PostsController : AdminController
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsController(IDeletableEntityRepository<Post> postsDeletableRepository)
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

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, UpdatePostViewModel inputPost)
        {
            Post postFromDatabase = null;

            if (ModelState.IsValid)
            {
                postFromDatabase = this.postsRepository.GetById(inputPost.Id);
                Mapper.CreateMap<UpdatePostViewModel, Post>();                                 
                Mapper.Map(inputPost, postFromDatabase);
                this.postsRepository.Update(postFromDatabase);
                this.postsRepository.SaveChanges();                
            }

            var modifedPostForKendo = Mapper.Map<ShowPostsViewModel>(postFromDatabase);

            return this.Json(new[] { modifedPostForKendo }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, DeletePostViewModel inputPost)
        {
            if (ModelState.IsValid)
            {
                this.postsRepository.Delete(inputPost.Id);
                this.postsRepository.SaveChanges();
            }

            return this.Json(new[] { inputPost }.ToDataSourceResult(request, this.ModelState));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var post = this.postsRepository
                .GetById(id.Value);

            if (post == null)
            {
                return this.HttpNotFound("Post not found");
            }

            var mappedPost = Mapper.Map<PostDetailsViewModel>(post);
            return View(mappedPost);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var post = this.postsRepository
                .GetById(id.Value);

            if (post == null)
            {
                return this.HttpNotFound("Post not found");
            }

            var mappedPost = Mapper.Map<EditPostViewModel>(post);
            return View(mappedPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPostViewModel inputPost)
        {
            if (ModelState.IsValid)
            {
                var postFromDatabase = this.postsRepository.GetById(inputPost.Id);
                Mapper.CreateMap<EditPostViewModel, Post>();
                Mapper.Map(inputPost, postFromDatabase);

                // TODO add to separate method and class
                if (inputPost.NewCoverPhoto != null)
                {
                    string picturePath = "/Content/UserFiles/Images/";
                    var cover = inputPost.NewCoverPhoto.FirstOrDefault();
                    string path = Path.Combine(Server.MapPath(picturePath), Path.GetFileName(cover.FileName));
                    cover.SaveAs(path);
                    postFromDatabase.CoverPhotoPath = picturePath + cover.FileName;
                }

                this.postsRepository.Update(postFromDatabase);
                this.postsRepository.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(inputPost);
        }
    }
}