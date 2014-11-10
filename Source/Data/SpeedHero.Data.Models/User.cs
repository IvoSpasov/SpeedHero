namespace SpeedHero.Data.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SpeedHero.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        // TODO How is user avatar supported in MVC?
        // all users may leave comments but not all may create posts. May be there should be another user?
        private ICollection<Post> posts;
        private ICollection<Comment> comments;


        // This will prevent UserManager.CreateAsync from causing exception
        public User()
        {
            this.CreatedOn = DateTime.Now;
            this.posts = new HashSet<Post>();
            this.comments = new HashSet<Comment>();
        }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // for fast db search
        [Index] 
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Post> Posts
        {
            get { return this.posts; }
            set { this.posts = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
