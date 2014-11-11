namespace SpeedHero.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SpeedHero.Data.Common.Models;

    public class Post : AuditInfo, IDeletableEntity
    {
        private ICollection<Comment> comments;
        private ICollection<Picture> pictures;

        public Post()
        {
            this.comments = new HashSet<Comment>();
            this.pictures = new HashSet<Picture>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(70)]
        [Required]
        public string Title { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        // May be this should be a seperate class.. and how and where to hold the pictures?
        // What about post cover?
        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<Picture> Pictures
        {
            get { return this.pictures; }
            set { this.pictures = value; }
        }

        // TODO Post also have Tags... Implement later?
    }
}
