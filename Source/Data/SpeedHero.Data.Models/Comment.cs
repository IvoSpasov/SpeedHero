namespace SpeedHero.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SpeedHero.Data.Common.Models;

    public class Comment : DeletableEntity, IAuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
