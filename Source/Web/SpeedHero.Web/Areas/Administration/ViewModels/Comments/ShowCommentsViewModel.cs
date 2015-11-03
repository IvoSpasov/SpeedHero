namespace SpeedHero.Web.Areas.Administration.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.ViewModels.Base;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class ShowCommentsViewModel : AdministrationViewModel, IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        [Display(Name = "Author")]
        public string AuthorUserName { get; set; }
    }
}