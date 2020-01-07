using System.ComponentModel.DataAnnotations;

namespace Bobkov.Web.Models.Blog
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Не указано имя категории")]
        public string Name { get; set; }
    }
}
