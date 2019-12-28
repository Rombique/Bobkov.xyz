using System.ComponentModel.DataAnnotations;

namespace Bobkov.Web.Models.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Не указан логин")]
        [Display(Name = "Логин")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Не указана почта")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string ConfirmPassword { get; set; }
    }
}
