using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajes.Models;
using ProyectoLenguajes.Models.Domain;
using ProyectoLenguajes.Models.DTO;
using ProyectoLenguajes.Repositories.Abstract;

namespace ProyectoLenguajes.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserAuthenticationController(IUserAuthenticationService service, UserManager<ApplicationUser> userManager)
        {
            this._service = service;
            this._userManager = userManager;
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Role = "user";
            var result = await _service.RegistrationAsync(model);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Registration));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _service.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _service.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Reg()
        {
            var model = new RegistrationModel
            {
                UserName = "admin_w",
                Name = "Wilson-Mata",
                Email = "wilsonbm@gmail.com",
                Password = "Admin2023*",
            };
            model.Role = "admin";
            var result = await _service.RegistrationAsync(model);
            return Ok(result);

        }
 
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        // El cambio de contraseña fue exitoso
                        return RedirectToAction("ChangePassword", "Home");
                    }
                    else
                    {
                        // Agrega los errores de validación al ModelState
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Usuario no encontrado");
                }
            }

            // Si llegamos hasta aquí, algo salió mal. Vuelve a mostrar la vista con los errores.
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUsername(ChangeUserName model)
        {
            if (!ModelState.IsValid)
            {
                // El modelo no es válido, retorna la vista EditProfile con el modelo para mostrar los errores.
                return View("EditProfile", model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // No se pudo encontrar el usuario, redirigir o mostrar un mensaje de error.
                return RedirectToAction("Index", "Home");
            }

            var currentUsername = user.UserName;
            var newUsername = model.NewUsername;

            if (currentUsername.Equals(newUsername, StringComparison.OrdinalIgnoreCase))
            {
                // El nuevo nombre de usuario es igual al nombre de usuario actual, no se realiza ningún cambio.
                ModelState.AddModelError(string.Empty, "El nuevo nombre de usuario debe ser diferente al nombre de usuario actual.");
                return View("EditProfile", model);
            }

            // Verifica si el nuevo nombre de usuario ya está en uso por otro usuario.
            var existingUser = await _userManager.FindByNameAsync(newUsername);
            if (existingUser != null)
            {
                // El nuevo nombre de usuario ya está en uso, muestra un error.
                ModelState.AddModelError(string.Empty, "El nombre de usuario ya está en uso. Por favor, elija otro.");
                return View("EditProfile", model);
            }

            // Cambia el nombre de usuario.
            user.UserName = newUsername;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                // El cambio de nombre de usuario no fue exitoso, agrega errores de validación.
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View("EditProfile", model);
            }

            // El cambio de nombre de usuario fue exitoso, puedes redirigir a una página de éxito o realizar otras acciones.
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(ChangeEmail model)
        {
            if (!ModelState.IsValid)
            {
                // El modelo no es válido, retorna la vista EditProfile con el modelo para mostrar los errores.
                return View("EditProfile", model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // No se pudo encontrar el usuario, redirigir o mostrar un mensaje de error.
                return RedirectToAction("Index", "Home");
            }

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
            var result = await _userManager.ChangeEmailAsync(user, model.NewEmail, token);
            if (!result.Succeeded)
            {
                // El cambio de correo electrónico no fue exitoso, agrega errores de validación.
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View("ChangeEmail", model);
            }

            // El cambio de correo electrónico fue exitoso, puedes redirigir a una página de éxito o realizar otras acciones.
            return RedirectToAction("Index", "Home");
        }

        

    }
}
