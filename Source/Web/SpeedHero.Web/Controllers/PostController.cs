namespace SpeedHero.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

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

        public ActionResult Post(int id)
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
    }
}