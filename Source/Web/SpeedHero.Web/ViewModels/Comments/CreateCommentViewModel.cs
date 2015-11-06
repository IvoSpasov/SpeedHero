namespace SpeedHero.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Data.Models;
    using Infrastructure.Mapping;

    public class CreateCommentViewModel : IMapFrom<Comment>
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }
    }
}