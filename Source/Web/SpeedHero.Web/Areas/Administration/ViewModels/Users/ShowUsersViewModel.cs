namespace SpeedHero.Web.Areas.Administration.ViewModels.Users
{
    using System;

    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.ViewModels.Base;
    using SpeedHero.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class ShowUsersViewModel : AdministrationViewModel, IMapFrom<User>
    {
        public string Id { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Registered on")]
        public override DateTime CreatedOn { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public int AccessFailedCount { get; set; }
    }
}