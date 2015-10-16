namespace SpeedHero.Web.Areas.Administration.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;
    using System.Web.Mvc;

    public class EditCommentViewModel : IMapFrom<Comment>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }
    }
}