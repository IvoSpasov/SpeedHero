namespace SpeedHero.Web.ViewModels.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    public class CreatePostViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        // [UIHint("tinymce_full")]
        [Required]
        [AllowHtml]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Cover Photo")]
        public IEnumerable<HttpPostedFileBase> CoverPhoto { get; set; }
    }
}