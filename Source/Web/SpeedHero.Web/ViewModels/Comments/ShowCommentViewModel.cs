namespace SpeedHero.Web.ViewModels.Comments
{
    using System;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class ShowCommentViewModel : IMapFrom<Comment>
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual User Author { get; set; }
    }
}