namespace SpeedHero.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class CreateCommentViewModel
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }
    }
}