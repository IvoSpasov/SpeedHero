namespace SpeedHero.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using SpeedHero.Data.Common.Repositories;
    using SpeedHero.Data.Models;
    using SpeedHero.Web.Areas.Administration.Controllers.Base;
    using SpeedHero.Web.Areas.Administration.ViewModels.Users;

    public class UsersController : AdminController
    {
        private readonly IDeletableEntityRepository<User> usersRepository;

        public UsersController(IDeletableEntityRepository<User> deletableEntityRepository)
        {
            this.usersRepository = deletableEntityRepository;
        }

        // GET: Administration/Users
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var users = this.usersRepository
                .All()
                .Project()
                .To<ShowUsersViewModel>();

            DataSourceResult result = users.ToDataSourceResult(request);

            return this.Json(result);
        }
    }
}