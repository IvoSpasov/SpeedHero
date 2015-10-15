namespace SpeedHero.Web.Areas.Administration.ViewModels.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class EditPostViewModel : IMapFrom<Post>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }

        [Display(Name = "New cover photo")]
        public IEnumerable<HttpPostedFileBase> NewCoverPhoto { get; set; }
    }
}