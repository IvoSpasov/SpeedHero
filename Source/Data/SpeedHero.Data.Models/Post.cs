namespace SpeedHero.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SpeedHero.Common.Constants;
    using SpeedHero.Data.Common.Models;

    public class Post : DeletableEntity, IAuditInfo, IDeletableEntity
    {
        private ICollection<Comment> comments;

        public Post()
        {
            this.comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.PostTitleMaxLength)]
        [Required]
        public string Title { get; set; }

        [DataType(DataType.Html)]
        public string Content { get; set; }

        public string CoverPhotoPath { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }        

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
