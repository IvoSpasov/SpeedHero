namespace SpeedHero.Web.Areas.Administration.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;
    using Data.Models;
    using Infrastructure.Mapping;

    public class UpdatePostViewModel : IMapFrom<Post>
    {
        // Server side validation
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.PostTitleMaxLength, MinimumLength = ValidationConstants.PostTitleMinLength)]
        public string Title { get; set; }
    }
}