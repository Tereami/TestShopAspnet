using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Models;

namespace TestShopAspnet.ViewModels
{
    public class PersonViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "От 2 до 50 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Неверный формат имени")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Не указана фамилия")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "От 2 до 50 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Неверный формат фамилии")]
        public string Surname { get; set; }

        [Range(18, 80, ErrorMessage = "От 18 до 80 лет")]
        public int Age { get; set; }

        [Display(Name = "Должность")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "От 5 до 50 символов")]
        public string Position { get; set; }

    }
}
