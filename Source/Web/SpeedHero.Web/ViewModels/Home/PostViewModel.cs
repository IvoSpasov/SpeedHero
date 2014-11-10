namespace SpeedHero.Web.ViewModels.Home
{
    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class PostViewModel : IMapFrom<Post>
    {
        public string Title { get; set; }
    }
}