namespace SpeedHero.Web.Controllers
{
    using System.Linq;
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
        public ActionResult CreatePost(Post createdPost) // use Post or PostViewModel?
        {
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