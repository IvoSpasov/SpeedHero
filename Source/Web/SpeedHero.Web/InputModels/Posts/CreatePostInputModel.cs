﻿namespace SpeedHero.Web.InputModels.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class CreatePostInputModel : IMapFrom<Post>
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        [AllowHtml]
        //[UIHint("tinymce_full")]
        public string Content { get; set; }

        [Required]
        public IEnumerable<HttpPostedFileBase> CoverPhoto { get; set; }
    }
}