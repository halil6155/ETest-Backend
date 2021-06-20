using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Entities.Abstract;

namespace ETest.Dto.User
{
    public class UserForLoginDto:IDto
    {
        [Required(ErrorMessage = "Email Alanı Zorunlu Alandır!")]
        [MaxLength(50, ErrorMessage = "Email Alanı Max. 50 Karakterdir!")]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Bu Alan Email Formatında Olmalıdır!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre Zorunlu Alandır!")]
        [DisplayName("Şifre")]
        [MaxLength(50, ErrorMessage = "Şifre Alanı Max. 50 Karakterdir!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}