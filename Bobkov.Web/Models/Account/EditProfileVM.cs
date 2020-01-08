using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Bobkov.Web.Models.Account
{
    public class EditProfileVM
    {
        public int Id { get; set; }
        public IFormFile Avatar { get; set; }
        public string OldAvatar { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
