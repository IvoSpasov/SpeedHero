namespace SpeedHero.Web.Areas.Administration.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.ViewModels.Base;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class ShowCommentsViewModel : AdministrationViewModel, IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        [Display(Name = "Author")]
        public string AuthorName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, ShowCommentsViewModel>()
                .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(c => c.Author.UserName));
        }
    }
}