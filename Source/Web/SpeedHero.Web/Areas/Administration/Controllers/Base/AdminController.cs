namespace SpeedHero.Web.Areas.Administration.Controllers.Base
{
    using System.Web.Mvc;

    // [Authorize(Roles = "Admin")] (for debbuging)
    [Authorize]
    public abstract class AdminController : Controller
    {
    }
}