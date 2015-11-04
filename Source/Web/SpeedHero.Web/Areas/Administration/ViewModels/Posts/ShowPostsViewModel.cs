namespace SpeedHero.Web.Areas.Administration.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Common.Constants;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.ViewModels.Base;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class ShowPostsViewModel : AdministrationViewModel, IMapFrom<Post>
    {
        // Client side validation (Kendo)
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required]
        [StringLength(ValidationConstants.PostTitleMaxLength, MinimumLength = ValidationConstants.PostTitleMinLength)]        
        public string Title { get; set; }

        [Display(Name = "Author")]
        [HiddenInput(DisplayValue = false)]
        public string AuthorUserName { get; set; }
    }
}