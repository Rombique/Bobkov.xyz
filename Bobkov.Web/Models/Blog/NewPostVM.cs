using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bobkov.Web.Models.Blog
{
    public class NewPostVM
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Не указан заголовок")]
        public string Title { get; set; }
        [Display(Name = "Превью контент")]
        [Required(ErrorMessage = "Не указано превью")]
        public string Preview { get; set; }
        [Display(Name = "Контент")]
        [Required(ErrorMessage = "Не указан контент")]
        public string Content { get; set; }
        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Не указан категория")]
        public int SelectedCategoryId { get; set; }
        public IEnumerable<CategoryVM> Categories { get; set; }
    }
}
