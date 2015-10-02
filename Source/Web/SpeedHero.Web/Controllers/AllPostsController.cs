namespace SpeedHero.Web.Controllers
{
    using SpeedHero.Data.Common.Repositories;
    using SpeedHero.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using SpeedHero.Web.ViewModels.Home;

    public class AllPostsController : Controller
    {
        private IGenericRepository<Post> allPosts;

        public AllPostsController(IGenericRepository<Post> allPosts)
        {
            this.allPosts = allPosts;
        }

        public ActionResult Index()
        {
            if (this.allPosts == null)
            {
                return this.Content("no postst in database");
            }

            var allPosts = this.allPosts
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .Project()
                .To<IndexViewModel>();

            return this.View(allPosts);
        }
    }
}