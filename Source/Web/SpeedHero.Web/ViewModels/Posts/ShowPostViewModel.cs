namespace SpeedHero.Web.ViewModels.Posts
{
    using System;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class ShowPostViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CoverPhotoPath { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public string AuthorName { get; set; }

        public int NumberOfComments { get; set; }
    }
}