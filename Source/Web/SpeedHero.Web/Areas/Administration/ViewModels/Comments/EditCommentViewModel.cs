namespace SpeedHero.Web.Areas.Administration.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class EditCommentViewModel : IMapFrom<Comment>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }
    }
}