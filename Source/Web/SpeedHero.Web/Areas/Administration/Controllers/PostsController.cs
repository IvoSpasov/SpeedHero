namespace SpeedHero.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using SpeedHero.Data.Common.Repositories;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.Controllers.Base;
    using SpeedHero.Web.Areas.Administration.ViewModels.Posts;
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
                .ProjectTo<ShowPostsViewModel>();

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
            if (inputPost.File != null && !this.CheckIsFileAnImage(inputPost.File))
            {
                ModelState.AddModelError("Cover photo", "Cover photo must be of type \"jpeg\" or \"png\".");
            }

            if (ModelState.IsValid)
            {
                var postFromDatabase = this.postsRepository.GetById(inputPost.Id);
                Mapper.CreateMap<EditPostViewModel, Post>();
                Mapper.Map(inputPost, postFromDatabase);

                if (inputPost.File != null)
                {
                    postFromDatabase.CoverPhotoPath = WebConstants.ImagesPath + inputPost.File.FileName;
                    this.SaveCoverPhoto(inputPost.File, WebConstants.ImagesPath);
                }

                this.postsRepository.Update(postFromDatabase);
                this.postsRepository.SaveChanges();

                return this.Redirect(returnUrl);
            }

            ViewBag.ModelState = "Invalid";
            return this.View(inputPost);
        }

        private bool CheckIsFileAnImage(HttpPostedFileBase file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("No file");
            }

            var allowedFileTypes = new List<string> { "image/jpeg", "image/png" };

            foreach (var type in allowedFileTypes)
            {
                if (file.ContentType == type)
                {
                    return true;
                }
            }

            return false;
        }

        private void SaveCoverPhoto(HttpPostedFileBase coverPhoto, string path)
        {
            if (coverPhoto == null)
            {
                throw new ArgumentNullException("No cover photo");
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("No path in which to save the files");
            }

            // Some browsers send file names with full path. We only care about the file name.
            var coverPhotoName = Path.GetFileName(coverPhoto.FileName);
            var destinationPath = Path.Combine(Server.MapPath(path), coverPhotoName);
            coverPhoto.SaveAs(destinationPath);
        }
    }
}