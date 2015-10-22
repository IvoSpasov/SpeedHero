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
            if (inputPost.CoverPhotoUrl == null && inputPost.File == null)
            {
                ModelState.AddModelError("Cover photo", "There must be a Cover photo specified either by URL or file on your pc.");
            }

            if (inputPost.CoverPhotoUrl != null && inputPost.File != null)
            {
                ModelState.AddModelError("Cover photo", "Please specify only one input for cover photo.");
            }


            if (inputPost.File != null && !this.CheckIsFileAnImage(inputPost.File))
            {
                ModelState.AddModelError("Cover photo", "Cover photo must be of type \"jpeg\" or \"png\".");
            }

            if (ModelState.IsValid)
            {
                // This is to remind me if I don't use kendo Editor and the content needs decoding/encoding before saving to DB.
                // string content = HttpUtility.HtmlDecode(inputPost.Content);

                Mapper.CreateMap<CreatePostViewModel, Post>();
                var newPost = Mapper.Map<Post>(inputPost);
                newPost.AuthorId = this.User.Identity.GetUserId();

                if (inputPost.CoverPhotoUrl != null)
                {
                    newPost.CoverPhotoPath = inputPost.CoverPhotoUrl;
                }
                else
                {
                    newPost.CoverPhotoPath = WebConstants.ImagesPath + inputPost.File.FileName;
                    this.SaveCoverPhoto(inputPost.File, WebConstants.ImagesPath);
                }

                this.postsRepository.Add(newPost);
                this.postsRepository.SaveChanges();
                this.TempData["SuccessfullNewPost"] = "Your post was successfully created.";
                return this.RedirectToAction("ShowPost", new { id = newPost.Id });
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