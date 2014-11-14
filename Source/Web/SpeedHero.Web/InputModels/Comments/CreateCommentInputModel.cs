namespace SpeedHero.Web.InputModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class CreateCommentInputModel : IMapFrom<Comment>
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}