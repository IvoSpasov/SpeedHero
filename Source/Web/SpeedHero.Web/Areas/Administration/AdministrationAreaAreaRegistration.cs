﻿namespace SpeedHero.Web.Areas.Administration
{
    using System.Web.Mvc;

    using SpeedHero.Web.Helpers;

    public class AdministrationAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return WebConstants.AdministrationAreaName;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Administration_default",
                url: WebConstants.AdministrationAreaName + "/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional });
        }
    }
}