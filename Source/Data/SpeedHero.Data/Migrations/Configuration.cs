namespace SpeedHero.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using SpeedHero.Common;
    using SpeedHero.Data.Models;

    public sealed class Configuration : DbMigrationsConfiguration<SpeedHeroDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;

            // TODO: Remove in production
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SpeedHeroDbContext context)
        {
            // This method will be called after migrating to the latest version.

            // You can use the DbSet<T>.AddOrUpdate() helper extension method 
            // to avoid creating duplicate seed data. E.g.
            //
            //   context.People.AddOrUpdate(
            //     p => p.FullName,
            //     new Person { FullName = "Andrew Peters" },
            //     new Person { FullName = "Brice Lambson" },
            //     new Person { FullName = "Rowan Miller" }
            //   );
            this.SeedRoles(context);
            this.SeedUsersWithRoles(context);
            this.SeedPosts(context);
        }

        protected void SeedRoles(SpeedHeroDbContext context)
        {
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole(GlobalConstants.AdministratorRoleName));
        }

        protected void SeedUsersWithRoles(SpeedHeroDbContext context)
        {
            const string AdminUserName = "admin";
            const string AdminEmail = "admin@admin.com";
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore); // to access user table but in different way than context

            if (!context.Users.Any(u => u.UserName == AdminUserName))
            {
                var user = new User { UserName = AdminUserName, Email = AdminEmail };
                userManager.Create(user, "123456");
                userManager.AddToRole(user.Id, GlobalConstants.AdministratorRoleName);
            }
        }

        protected void SeedPosts(SpeedHeroDbContext context)
        {
            if (!context.Posts.Any())
            {
                string lorem1 = "<p>Lorem ipsum dolor sit amet, vix in audiam bonorum legimus, nostrud consulatu molestiae vix an. No per dicta equidem qualisque, ei has sententiae consectetuer definitionem. Ad vero illud tritani cum. At eirmod utroque mel, cu ius tollit electram.</p>";
                string lorem2 = "<p>In sit veniam percipit. No sed augue tempor nostro, ei odio enim usu, ocurreret explicari reformidans mel an. Ornatus tincidunt ne vis. Utinam ullamcorper qui at, ei elitr ubique disputationi sit.</p>";

                var firstPost = new Post
                {
                    Title = "Hachiroku",
                    Content = lorem1 + lorem2 + lorem1,
                    CoverPhotoPath = "http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/01-hachiroku_zpsenu9glp0.jpg",
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin")
                };

                var secondPost = new Post
                {
                    Title = "Toyota GT86",
                    Content = lorem1 + lorem2 + lorem1,
                    CoverPhotoPath = "http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/03-GT86_zpsudccrtla.jpg",
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin")
                };

                var thirdPost = new Post
                {
                    Title = "Project Toyota Supra",
                    Content = lorem1 + lorem2 + lorem1,
                    CoverPhotoPath = "http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/06-Supra_zpssa9lvyfb.jpg",
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin")
                };

                var fourthPost = new Post
                {
                    Title = "Slammed and stanced VIP",
                    Content = lorem1 + lorem2 + lorem1,
                    CoverPhotoPath = "http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/07-VIP_zpshqw2f2rq.jpg",
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin")
                };

                var fifthPost = new Post
                {
                    Title = "Skyline R35",
                    Content = lorem1 + lorem2 + lorem1,
                    CoverPhotoPath = "http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/08-R35_zpswmu6tdui.jpg",
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin")
                };

                var sixthPost = new Post
                {
                    Title = "Hakosuka",
                    Content = lorem1 + lorem2 + lorem1,
                    CoverPhotoPath = "http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/09-hakosuka_zpsrs2wlvqy.jpg",
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin")
                };

                var seventhPost = new Post
                {
                    Title = "Hachiroku",
                    Content = lorem1 + lorem2 + lorem1,
                    CoverPhotoPath = "http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/02-hachiroku_zpsqxzenvs8.jpg",
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin")
                };

                var eightPost = new Post
                {
                    Title = "Skyline R34",
                    Content = lorem1 + lorem2 + lorem1,
                    CoverPhotoPath = "http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/10-R34_zpsffdwcs3w.jpg",
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin")
                };


                var ninthPost = new Post
                {
                    Title = "Toyota GT86",
                    Content = lorem1 + lorem2 + lorem1,
                    CoverPhotoPath = "http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/12-GT86_zpsznaaahdc.jpg",
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin")
                };

                var tenthPost = new Post
                {
                    Title = "Silvia S15",
                    Content = lorem1 + lorem2 + lorem1 + ReturnImageTag("http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/S15/02-S15_zpsmybkvyt7.jpg") + lorem2 + lorem1,
                    CoverPhotoPath = "http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/S15/01-S15_zpskuqclnmc.jpg",
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin")
                };

                var eleventhPost = new Post
                {
                    Title = "Rocket Bunny FD RX7",
                    Content = lorem1 + lorem2 + lorem1 + this.ReturnImageTag("http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/RX7/02.%20Rx7_zpsbv4vzigw.jpg") + lorem2 + lorem1 +
                    this.ReturnImageTag("http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/RX7/03.%20Rx7_zpskal9e90c.jpg"),
                    CoverPhotoPath = "http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/RX7/01.%20Rx7_zpsxww11r3u.jpg",
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin")
                };

                var twelfthPost = new Post
                {
                    Title = "Lexus Rc 350",
                    Content = lorem1 + lorem2 + lorem1 + this.ReturnImageTag("http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/RC/13-11-05-lexus-rc-front-full_zpsjw5aq0xp.jpg") + lorem2 + lorem1 +
                    this.ReturnImageTag("http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/RC/13-11-05-lexus-rc-rear-full_zpslh6bu8l4.jpg") + lorem2 + lorem2,
                    CoverPhotoPath = "http://i571.photobucket.com/albums/ss160/ivossss/SpeedHero/RC/13-11-05-lexus-rc-side-full_zps9mmge9l8.jpg",
                    Author = context.Users.FirstOrDefault(u => u.UserName == "admin")
                };


                context.Posts.AddOrUpdate(p => p.Title, twelfthPost);
                context.Posts.AddOrUpdate(p => p.Title, eleventhPost);
                context.Posts.AddOrUpdate(p => p.Title, tenthPost);
                context.Posts.AddOrUpdate(p => p.Title, ninthPost);
                context.Posts.AddOrUpdate(p => p.Title, eightPost);
                context.Posts.AddOrUpdate(p => p.Title, seventhPost);
                context.Posts.AddOrUpdate(p => p.Title, sixthPost);
                context.Posts.AddOrUpdate(p => p.Title, fifthPost);
                context.Posts.AddOrUpdate(p => p.Title, fourthPost);
                context.Posts.AddOrUpdate(p => p.Title, thirdPost);
                context.Posts.AddOrUpdate(p => p.Title, secondPost);
                context.Posts.AddOrUpdate(p => p.Title, firstPost);
            }
        }

        private string ReturnImageTag(string imageUrl)
        {
            return string.Format("<p><img style=\"display: block; margin-left: auto; margin-right: auto;\" alt src=\"{0}\"></p>", imageUrl);
        }
    }
}
