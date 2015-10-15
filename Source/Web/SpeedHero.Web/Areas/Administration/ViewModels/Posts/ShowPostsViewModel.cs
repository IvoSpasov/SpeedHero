namespace SpeedHero.Web.Areas.Administration.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.ViewModels.Base;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class ShowPostsViewModel : AdministrationViewModel, IMapFrom<Post>, IHaveCustomMappings
    {
        // Client side validation (Kendo)
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required]
        [StringLength(100, MinimumLength = 5)]        
        public string Title { get; set; }

        [Display(Name = "Author")]
        [HiddenInput(DisplayValue = false)]
        public string AuthorName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Post, ShowPostsViewModel>()
                .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(p => p.Author.UserName));
        }
    }
}