namespace SpeedHero.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    public class CreatePostViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
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