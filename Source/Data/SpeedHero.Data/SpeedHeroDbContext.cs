namespace SpeedHero.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using SpeedHero.Data.Models;

    public class SpeedHeroDbContext : IdentityDbContext<User>
    {
        public SpeedHeroDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static SpeedHeroDbContext Create()
        {
            return new SpeedHeroDbContext();
        }
    }
}
