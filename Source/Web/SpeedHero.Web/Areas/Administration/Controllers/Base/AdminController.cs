namespace SpeedHero.Web.Areas.Administration.Controllers.Base
{
    using System.Web.Mvc;
    
    using Common.Constants;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public abstract class AdminController : Controller
    {
    }
}