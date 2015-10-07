namespace SpeedHero.Web.App_Start
{
    using System.Web.Mvc;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            
            // Global authorization filter
            // filters.Add(new AuthorizeAttribute { Roles = "Admin" });
        }
    }
}
