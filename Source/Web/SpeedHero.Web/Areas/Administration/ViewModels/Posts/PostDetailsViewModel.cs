namespace SpeedHero.Web.Areas.Administration.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class PostDetailsViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [Display(Name = "Cover photo path")]
        public string CoverPhotoPath { get; set; }

        [Display(Name = "Author name")]
        public string AuthorName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Post, PostDetailsViewModel>()
                .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(p => p.Author.UserName));
        }
    }
}