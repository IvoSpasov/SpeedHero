namespace SpeedHero.Web.Areas.Administration.ViewModels.Comments
{
    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class ShowCommentsViewModel : IMapFrom<Comment>
    {
        public string Content { get; set; }
    }
}