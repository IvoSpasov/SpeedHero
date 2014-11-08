namespace SpeedHero.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using SpeedHero.Data.Models;
    using SpeedHero.Data.Migrations;
    using System.Data.Entity;

    public class SpeedHeroDbContext : IdentityDbContext<User>
    {
        public SpeedHeroDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SpeedHeroDbContext, Configuration>());
        }

        public static SpeedHeroDbContext Create()
        {
            return new SpeedHeroDbContext();
        }
    }
}
