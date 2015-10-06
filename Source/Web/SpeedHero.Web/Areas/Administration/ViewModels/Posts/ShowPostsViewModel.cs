namespace SpeedHero.Web.Areas.Administration.ViewModels.Posts
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.ViewModels.Base;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class ShowPostsViewModel : AdministrationViewModel, IMapFrom<Post>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(100, MinimumLength = 5)]
        public string Title { get; set; }

        [Display(Name = "Author")]
        [HiddenInput(DisplayValue = false)]
        public string AuthorName { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string CoverPhotoPath { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<Post, ShowPostsViewModel>()
                .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(p => p.Author.UserName));
        }
    }
}