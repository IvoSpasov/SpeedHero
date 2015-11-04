namespace SpeedHero.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using Common.Constants;
    using Microsoft.AspNet.Identity;
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

            if (inputPost.File != null && !KendoUpload.CheckIsFileAnImage(inputPost.File))
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
                    KendoUpload.SaveCoverPhoto(inputPost.File, WebConstants.ImagesPath, this.Server);
                }

                this.postsRepository.Add(newPost);
                this.postsRepository.SaveChanges();
                this.TempData["SuccessfullNewPost"] = "Your post was successfully created.";
                return this.RedirectToAction("ShowPost", new { id = newPost.Id });
            }

            ViewBag.ModelState = "Invalid";
            return this.View(inputPost);
        }
    }
}