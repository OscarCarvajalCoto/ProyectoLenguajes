using ProyectoLenguajes.Models.Domain;

namespace ProyectoLenguajes.Models
{
    public class UserInformation
    {
        public ApplicationUser User { get; set; }
        public List<string> Roles { get; set; }
    }
}
