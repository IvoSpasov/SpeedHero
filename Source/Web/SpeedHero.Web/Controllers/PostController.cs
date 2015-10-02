namespace SpeedHero.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNet.Identity;

    using SpeedHero.Data.Common.Repositories;
    using SpeedHero.Data.Models;

    using SpeedHero.Web.Infrastructure;
    using SpeedHero.Web.InputModels.Posts;
    using SpeedHero.Web.ViewModels.Posts;

    public class PostController : Controller
    {
        private readonly IGenericRepository<Post> posts;
        private readonly ISanitizer sanitizer;

        public PostController(IGenericRepository<Post> posts, ISanitizer sanitizer)
        {
            this.posts = posts;
            this.sanitizer = sanitizer;
        }

        // [AllowAnonymous]
        [HttpGet]
        public ActionResult ShowPost(int id)
        {
            var selectedPost = this.posts
                .All()
                .Where(p => p.Id == id)
                .Project()
                .To<ShowPostViewModel>()
                .FirstOrDefault();

            if (selectedPost == null)
            {
                this.HttpNotFound("Blog post not found");
            }

            return this.View(selectedPost);
        }
        
        // [Authorize(Roles = "Admin", "Lecturer")]
        // [Authorize(Users="Ivo")]
        [HttpGet]
        [Authorize]
        public ActionResult CreatePost()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(CreatePostInputModel inputPost)
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
                    string picturesPath = "/Content/UserFiles/Images/";

                    var cover = inputPost.CoverPhoto.FirstOrDefault();
                    string path = Path.Combine(Server.MapPath(picturesPath), Path.GetFileName(cover.FileName));
                    cover.SaveAs(path);
                    post.CoverPhotoPath = picturesPath + cover.FileName;
                }

                this.posts.Add(post);
                this.posts.SaveChanges();
                this.TempData["SuccessfullNewPost"] = "Well done! Your post was successfully created.";
                return this.RedirectToAction("ShowPost", new { id = post.Id });
            }

            ViewBag.ModelState = "Invalid";
            return this.View(inputPost);
        }
    }
}