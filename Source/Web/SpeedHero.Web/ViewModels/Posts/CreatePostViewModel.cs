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


        [Display(Name = "Cover photo url")]
        public string CoverPhotoUrl { get; set; }

        [Required]
        [Display(Name = "Cover photo")]
        public HttpPostedFileBase File { get; set; }
    }
}