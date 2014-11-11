namespace SpeedHero.Web.Controllers
{
    using System.Linq;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNet.Identity;

    using SpeedHero.Data.Common.Repository;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.ViewModels.Home;

    public class PostController : Controller
    {
        private IRepository<Post> posts;

        public PostController(IRepository<Post> posts)
        {
            this.posts = posts;
        }

        [HttpGet]
        public ActionResult ShowPost(int id)
        {
            var selectedPost = this.posts
                .All()
                .Where(p => p.IsDeleted == false)
                .Where(p => p.Id == id)
                .Project()
                .To<PostViewModel>()
                .FirstOrDefault();

            if (selectedPost == null)
            {
                this.HttpNotFound("Blog post not found");
            }

            return View(selectedPost);
        }

        [HttpGet]
        public ActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(Post createdPost, HttpPostedFileBase file) // use Post or PostViewModel?
        {
            if (file != null && file.ContentLength > 0)
            {
                string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
 
                Picture currentPicture = new Picture
                {
                    Name = file.FileName,
                    Path = "/Images/" + file.FileName
                };
                createdPost.Pictures.Add(currentPicture);
            }

            // TODO Check if the user is authenticated
            var currentUser = this.User.Identity.GetUserId();
            createdPost.AuthorId = currentUser;

            if (ModelState.IsValid)
            {
                this.posts.Add(createdPost);
                this.posts.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}