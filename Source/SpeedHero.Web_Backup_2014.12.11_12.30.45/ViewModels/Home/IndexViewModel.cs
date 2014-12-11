namespace SpeedHero.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;
    using System.Web.Mvc;

    public class IndexViewModel : IMapFrom<Post>
    {
        [HiddenInput(DisplayValue = false)] // what is that for?
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CoverPhotoPath { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}