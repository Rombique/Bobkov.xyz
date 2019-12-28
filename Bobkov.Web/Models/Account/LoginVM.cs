using System.ComponentModel.DataAnnotations;

namespace Bobkov.Web.Models.Account
{
    public class LoginVM
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Не указан логин")]
        public string Username { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить пароль?")]
        public bool RememberPassword { get; set; }
    }
}
