namespace SpeedHero.Web.Areas.Administration.ViewModels.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public abstract class AdministrationViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Created on")]
        [HiddenInput(DisplayValue = false)]
        public virtual DateTime CreatedOn { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Last modified on")]
        [HiddenInput(DisplayValue = false)]
        public DateTime? ModifiedOn { get; set; }
    }
}