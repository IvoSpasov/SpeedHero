namespace SpeedHero.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCommentViewModel
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}