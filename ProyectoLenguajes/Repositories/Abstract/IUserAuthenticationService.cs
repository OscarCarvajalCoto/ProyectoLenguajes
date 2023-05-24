﻿using ProyectoLenguajes.Models.DTO;

namespace ProyectoLenguajes.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task<Status> RegistrationAsync(RegistrationModel model);
        Task LogoutAsync();

    }
}