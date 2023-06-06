using System.ComponentModel.DataAnnotations;

namespace ProyectoLenguajes.Models.DTO
{
    public class ChangeUserName
    {
        [Required(ErrorMessage = "El nuevo nombre de usuario es obligatorio.")]
        [Display(Name = "Nuevo nombre de usuario")]
        public string NewUsername { get; set; }
    }
}
