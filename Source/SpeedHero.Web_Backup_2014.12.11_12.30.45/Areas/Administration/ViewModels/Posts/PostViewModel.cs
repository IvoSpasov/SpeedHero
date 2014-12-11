namespace SpeedHero.Web.Areas.Administration.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.ViewModels.Base;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class PostViewModel : AdministrationViewModel, IMapFrom<Post>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(100, MinimumLength = 5)]
        public string Title { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Author Id")]
        public string AuthorId { get; set; }

        //[Display(Name = "Author")]
        //[HiddenInput(DisplayValue = false)]
        //public virtual User Author { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        public string CoverPhotoPath { get; set; }

        // public virtual ICollection<Comment> Comments { get; set; }
    }
}