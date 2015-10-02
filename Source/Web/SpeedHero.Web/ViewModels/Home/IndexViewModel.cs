﻿namespace SpeedHero.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class IndexViewModel : IMapFrom<Post>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CoverPhotoPath { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}