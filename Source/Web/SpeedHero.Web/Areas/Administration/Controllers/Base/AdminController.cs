namespace SpeedHero.Web.Areas.Administration.Controllers.Base
{    
    using System.Web.Mvc;

    using SpeedHero.Common;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public abstract class AdminController : Controller
    {
    }
}