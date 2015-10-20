namespace SpeedHero.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using Microsoft.AspNet.Identity;

    using SpeedHero.Common;
    using SpeedHero.Data.Common.Repositories;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.Helpers;
    using SpeedHero.Web.ViewModels.Posts;

    public class PostController : Controller
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostController(IDeletableEntityRepository<Post> postsDeletableRepository)
        {
            this.postsRepository = postsDeletableRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ShowPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var selectedPost = this.postsRepository
                .GetById(id.Value);

            if (selectedPost == null)
            {
                return this.HttpNotFound("Post not found");
            }

            var mappedPost = Mapper.Map<ShowPostViewModel>(selectedPost);
            return this.View(mappedPost);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public ActionResult CreatePost()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(CreatePostViewModel inputPost)
        {
            if (ModelState.IsValid)
            {
                // This is to remind me if I don't use kendo Editor and the content needs decoding/encoding before saving to DB.
                // string content = HttpUtility.HtmlDecode(inputPost.Content);

                Mapper.CreateMap<CreatePostViewModel, Post>();
                var newPost = Mapper.Map<Post>(inputPost);
                newPost.AuthorId = this.User.Identity.GetUserId();
                newPost.CoverPhotoPath = WebConstants.ImagesPath + this.GetCoverPhotoName(inputPost.Files);
                this.SavePhoto(inputPost.Files, WebConstants.ImagesPath);
                this.postsRepository.Add(newPost);
                this.postsRepository.SaveChanges();
                this.TempData["SuccessfullNewPost"] = "Your post was successfully created.";
                return this.RedirectToAction("ShowPost", new { id = newPost.Id });
            }

            ViewBag.ModelState = "Invalid";
            return this.View(inputPost);
        }

        // TODO check if it's a picture or not
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