namespace SpeedHero.Web.ViewModels.Comments
{
    using System;
    using System.Linq;

    using AutoMapper;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class ShowCommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<Comment, ShowCommentViewModel>()
                .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(c => c.Author.UserName));
        }
    }
}