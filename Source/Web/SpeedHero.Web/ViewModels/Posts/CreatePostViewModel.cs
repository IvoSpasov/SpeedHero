namespace SpeedHero.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using Common.Constants;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CreatePostViewModel : IMapFrom<Post>
    {
        [Required]
        [StringLength(ValidationConstants.PostTitleMaxLength, MinimumLength = ValidationConstants.PostTitleMinLength)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }

        [Display(Name = "Cover photo URL")]
        public string CoverPhotoUrl { get; set; }

        [Display(Name = "Cover photo file")]
        public HttpPostedFileBase File { get; set; }
    }
}