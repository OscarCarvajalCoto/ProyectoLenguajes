using System.ComponentModel.DataAnnotations;

namespace ProyectoLenguajes.Models.DTO
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
