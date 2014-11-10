﻿namespace SpeedHero.Data.Models
{
    using SpeedHero.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Post : AuditInfo
    {
        private ICollection<Comment> comments;

        public Post()
        {
            this.comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        // May be this should be a seperate class.. and how and where to hold the pictures?
        // What about post cover?
        public string Content { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        // TODO Post also have Tags... Implement later?
    }
}