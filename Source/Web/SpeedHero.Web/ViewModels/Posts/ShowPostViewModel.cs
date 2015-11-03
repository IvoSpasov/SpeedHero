namespace SpeedHero.Web.ViewModels.Posts
{
    using System;
    using System.Linq;

    using AutoMapper;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class ShowPostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CoverPhotoPath { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public string AuthorUserName { get; set; }

        public int NumberOfComments { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Post, ShowPostViewModel>()
                //.ForMember(dto => dto.AuthorName, opt => opt.MapFrom(p => p.Author.UserName))
                .ForMember(dto => dto.NumberOfComments, opt => opt.MapFrom(p => p.Comments.Count()));
        }
    }
}