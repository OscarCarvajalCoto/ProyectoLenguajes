using System.ComponentModel.DataAnnotations;

namespace ProyectoLenguajes.Models.DTO
{
    public class ChangeEmail
    {
        [Required(ErrorMessage = "El nuevo correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        [Display(Name = "Nuevo correo electrónico")]
        public string NewEmail { get; set; }
    }
}
