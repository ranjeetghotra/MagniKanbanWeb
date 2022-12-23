using System.ComponentModel.DataAnnotations;

namespace MagniKanbanWeb.Models.Auth
{
    public class RegisterModel
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]

        public string UserName { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
