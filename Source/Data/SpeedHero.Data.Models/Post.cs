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
        private ICollection<TextPart> textParts;

        public Post()
        {
            this.comments = new HashSet<Comment>();
            this.pictures = new HashSet<Picture>();
            this.textParts = new HashSet<TextPart>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(70)]
        [Required]
        public string Title { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        // TODO: Remove Content!
        // public string Content { get; set; }

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

        public virtual ICollection<TextPart> TextParts
        {
            get { return this.textParts; }
            set { this.textParts = value; }
        }

        // TODO Post also have Tags... Implement later?
    }
}
