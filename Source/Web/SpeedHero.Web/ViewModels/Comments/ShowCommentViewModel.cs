namespace SpeedHero.Web.ViewModels.Comments
{
    using System;

    using AutoMapper;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class ShowCommentViewModel : IMapFrom<Comment>//, IHaveCustomMappings
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorUserName { get; set; }

        //public void CreateMappings(IConfiguration configuration)
        //{
        //    configuration.CreateMap<Comment, ShowCommentViewModel>()
        //        .ForMember(dto => dto.AuthorUserName, opt => opt.MapFrom(c => c.Author.UserName));
        //}
    }
}