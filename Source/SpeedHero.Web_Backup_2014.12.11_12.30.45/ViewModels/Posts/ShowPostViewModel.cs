﻿
namespace SpeedHero.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class ShowPostViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CoverPhotoPath { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public virtual User Author { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}