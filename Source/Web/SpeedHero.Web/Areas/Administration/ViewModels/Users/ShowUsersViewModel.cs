namespace SpeedHero.Web.Areas.Administration.ViewModels.Users
{
    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.ViewModels.Base;
    using SpeedHero.Web.Infrastructure.Mapping;

    public class ShowUsersViewModel : AdministrationViewModel, IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public int AccessFailedCount { get; set; }
    }
}