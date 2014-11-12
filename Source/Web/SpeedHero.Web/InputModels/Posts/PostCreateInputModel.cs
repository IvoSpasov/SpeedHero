namespace SpeedHero.Web.InputModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class PostCreateInputModel : IMapFrom<Post>
    {
        [Required]
        public string Title { get; set; }
        
        //[UIHint("tinymce")]
        public string Content { get; set; }
    }
}