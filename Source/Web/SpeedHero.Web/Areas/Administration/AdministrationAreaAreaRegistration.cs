namespace SpeedHero.Web.Areas.Administration
{
    using System.Web.Mvc;

    public class AdministrationAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Administration_default",
                url: "Administration/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional });
        }
    }
}