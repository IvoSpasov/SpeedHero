namespace SpeedHero.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using Microsoft.AspNet.Identity;
    
    using SpeedHero.Common;
    using SpeedHero.Data.Common.Repositories;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure;
    using SpeedHero.Web.ViewModels.Posts;

    public class PostController : Controller
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;
        private readonly ISanitizer sanitizer;

        public PostController(IDeletableEntityRepository<Post> postsDeletableRepository, ISanitizer sanitizer)
        {
            this.postsRepository = postsDeletableRepository;
            this.sanitizer = sanitizer;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ShowPost(int id)
        {
            var selectedPost = this.postsRepository
                .GetById(id);

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
                var currentUserId = this.User.Identity.GetUserId();
                string content = HttpUtility.HtmlDecode(inputPost.Content);

                var post = new Post
                {
                    Title = inputPost.Title,
                    Content = this.sanitizer.Sanitize(content),
                    AuthorId = currentUserId
                };

                if (inputPost.CoverPhoto != null)
                {
                    string picturePath = "/Content/UserFiles/Images/";
                    var cover = inputPost.CoverPhoto.FirstOrDefault();
                    string path = Path.Combine(Server.MapPath(picturePath), Path.GetFileName(cover.FileName));
                    cover.SaveAs(path);
                    post.CoverPhotoPath = picturePath + cover.FileName;
                }

                this.postsRepository.Add(post);
                this.postsRepository.SaveChanges();
                this.TempData["SuccessfullNewPost"] = "Your post was successfully created.";
                return this.RedirectToAction("ShowPost", new { id = post.Id });
            }

            ViewBag.ModelState = "Invalid";
            return this.View(inputPost);
        }
    }
}