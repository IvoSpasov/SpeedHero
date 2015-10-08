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
    }
}
