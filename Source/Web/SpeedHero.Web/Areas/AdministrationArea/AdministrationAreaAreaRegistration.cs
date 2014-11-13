using System.Web.Mvc;

namespace SpeedHero.Web.Areas.AdministrationArea
{
    public class AdministrationAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdministrationArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdministrationArea_default",
                "AdministrationArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}