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
    using System.Collections.Generic;
    using System.Web;
    using SpeedHero.Web.Helpers;

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
            return this.View(mappedPost);
        }

        public ActionResult Edit(int? id, string returnUrl)
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

            ViewBag.ReturnUrl = returnUrl;
            var mappedPost = Mapper.Map<EditPostViewModel>(post);
            return this.View(mappedPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPostViewModel inputPost, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var postFromDatabase = this.postsRepository.GetById(inputPost.Id);
                Mapper.CreateMap<EditPostViewModel, Post>();
                Mapper.Map(inputPost, postFromDatabase);

                if (inputPost.Files != null)
                {
                    postFromDatabase.CoverPhotoPath = WebConstants.ImagesPath + this.GetCoverPhotoName(inputPost.Files);
                    this.SavePhoto(inputPost.Files, WebConstants.ImagesPath);
                }

                this.postsRepository.Update(postFromDatabase);
                this.postsRepository.SaveChanges();

                return this.Redirect(returnUrl);
            }

            return this.View(inputPost);
        }

        protected void SavePhoto(IEnumerable<HttpPostedFileBase> files, string path)
        {
            if (files == null)
            {
                throw new ArgumentNullException("No collection of files");
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("No path in which to save the files");
            }

            var coverPhoto = files.FirstOrDefault();

            // Some browsers send file names with full path. We only care about the file name.
            var fileName = Path.GetFileName(coverPhoto.FileName);
            var destinationPath = Path.Combine(Server.MapPath(path), fileName);
            coverPhoto.SaveAs(destinationPath);
        }

        protected string GetCoverPhotoName(IEnumerable<HttpPostedFileBase> files)
        {
            if (files == null)
            {
                throw new ArgumentNullException("No collection of files");
            }

            var coverPhoto = files.FirstOrDefault();
            return coverPhoto.FileName;
        }
    }
}