using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bobkov.Web.Models.Blog
{
    public class CategoryVM
    {
        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Не указано имя категории")]
        public string Name { get; set; }
    }
}
