namespace SpeedHero.Web.InputModels.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class CreatePostInputModel : IMapFrom<Post>
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [Display(Name = "Title")]
        public string Title { get; set; }
        
        [Required]
        [AllowHtml]
        //[UIHint("tinymce_full")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Cover Photo")]
        public IEnumerable<HttpPostedFileBase> CoverPhoto { get; set; }
    }
}