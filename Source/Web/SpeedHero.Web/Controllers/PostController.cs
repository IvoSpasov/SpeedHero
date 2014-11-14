namespace SpeedHero.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNet.Identity;

    using SpeedHero.Data.Common.Repository;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure;
    using SpeedHero.Web.InputModels.Posts;
    using SpeedHero.Web.ViewModels.Home;
    using SpeedHero.Web.ViewModels.Posts;

    public class PostController : Controller
    {
        private readonly IRepository<Post> posts;
        private readonly ISanitizer sanitizer;


        public PostController(IRepository<Post> posts, ISanitizer sanitizer)
        {
            this.posts = posts;
            this.sanitizer = sanitizer;
        }

        [HttpGet]
        public ActionResult ShowPost(int id)
        {
            var selectedPost = this.posts
                .All()
                .Where(p => p.IsDeleted == false)
                .Where(p => p.Id == id)
                .Project()
                .To<ShowPostViewModel>()
                .FirstOrDefault();

            if (selectedPost == null)
            {
                this.HttpNotFound("Blog post not found");
            }

            return View(selectedPost);
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreatePost()
        {
            return View();
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

                this.posts.Add(post);
                this.posts.SaveChanges();
                return this.RedirectToAction("ShowPost", new { id = post.Id });
            }

            return this.View(inputPost);

                        
            //if (file != null && file.ContentLength > 0)
            //{
            //    string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
            //    file.SaveAs(path);

            //    Picture currentPicture = new Picture
            //    {
            //        Name = file.FileName,
            //        Path = "/Images/" + file.FileName,
            //        SerialNumber = 1
            //    };
            //    inputPost.Pictures.Add(currentPicture);
            //}
        }

        [HttpGet]
        public ActionResult EditPost(int id)
        {
            var currentPost = this.posts
                .All()
                .Where(p => p.Id == id)
                .Project()
                .To<IndexViewModel>()
                .FirstOrDefault();

            return View(currentPost);
        }

        [HttpPost]
        public ActionResult EditPost(int id, TextPart currentTextPart)
        {
            var currentPost = this.posts
                .All()
                .Where(p => p.Id == id)
                .Project()
                .To<IndexViewModel>()
                .FirstOrDefault();

            return View();
        }
    }
}