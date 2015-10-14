namespace SpeedHero.Web.Areas.Administration.ViewModels.Posts
{
    using AutoMapper;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class PostDetailsViewModel: IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CoverPhotoPath { get; set; }

        public string AuthorName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<Post, ShowPostsViewModel>()
                .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(p => p.Author.UserName));
        }
    }
}