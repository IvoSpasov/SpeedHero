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
                newPost.CoverPhotoPath = this.CreateCoverPhotoPath(inputPost.CoverPhoto);
                this.postsRepository.Add(newPost);
                this.postsRepository.SaveChanges();
                this.TempData["SuccessfullNewPost"] = "Your post was successfully created.";
                return this.RedirectToAction("ShowPost", new { id = newPost.Id });
            }

            ViewBag.ModelState = "Invalid";
            return this.View(inputPost);
        }

        private string CreateCoverPhotoPath(IEnumerable<HttpPostedFileBase> coverPhoto)
        {
            if (coverPhoto == null)
            {
                throw new ArgumentNullException("Invalid cover photo");
            }

            string picturePath = "/Content/UserFiles/Images/";
            var cover = coverPhoto.FirstOrDefault();
            string path = Path.Combine(Server.MapPath(picturePath), Path.GetFileName(cover.FileName));
            cover.SaveAs(path);
            return picturePath + cover.FileName;
        }
    }
}