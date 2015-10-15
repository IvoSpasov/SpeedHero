namespace SpeedHero.Web.ViewModels.Home
{
    using System;
    using System.Linq;

    using AutoMapper;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class IndexViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CoverPhotoPath { get; set; }

        public int NumberOfComments { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Post, IndexViewModel>()
                .ForMember(dto => dto.NumberOfComments, opt => opt.MapFrom(p => p.Comments.Count()));
        }
    }
}