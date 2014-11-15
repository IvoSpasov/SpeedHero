namespace SpeedHero.Web.Areas.Administration.ViewModels.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public abstract class AdministrationViewModel
    {
        [Display(Name = "Created On")]
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Modified On")]
        [HiddenInput(DisplayValue = false)]
        public DateTime? ModifiedOn { get; set; }

        // Изтритите данни не се визуализират в администрацията ив уеб проекта! Те стоят за стаистика и разследване само в базата!
        // Прави се отделна администрация ако искаш да се виждат и изтритите данни
    }
}