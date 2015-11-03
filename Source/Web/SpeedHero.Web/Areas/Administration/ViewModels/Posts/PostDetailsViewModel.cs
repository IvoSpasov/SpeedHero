namespace SpeedHero.Web.Areas.Administration.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.ViewModels.Base;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class PostDetailsViewModel : AdministrationViewModel, IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [Display(Name = "Cover photo path")]
        public string CoverPhotoPath { get; set; }

        [Display(Name = "Author")]
        public string AuthorUserName { get; set; }
    }
}