using Microsoft.AspNetCore.Identity;
using ProyectoLenguajes.Models.DTO;

namespace ProyectoLenguajes.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task<Status> RegistrationAsync(RegistrationModel model);
        Task LogoutAsync();

        Task<IdentityResult> changePasswordAsync(ChangePasswordModel model);
        Task<IdentityResult> changeEmailAsync(ChangePasswordModel model);
        Task<IdentityResult> changeUserNameAsync(ChangePasswordModel model);
    }
}
