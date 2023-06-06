using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajes.Models;
using ProyectoLenguajes.Models.Domain;
using ProyectoLenguajes.Models.DTO;
using ProyectoLenguajes.Repositories.Abstract;
using System.Net.Mail;

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
            model.Role = "client";
            var result = await _service.RegistrationAsync(model);
            TempData["msg"] = result.Message;
            string subject = "User Registration Message";
            string body = getEmailBody(model.Name, model.UserName, model.Password, model.Role);
            SendEmail(model.Email, subject, body);
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

        public void SendEmail(string destinationEmail, string subject, string message)
        {
            try
            {
                MailMessage email = new MailMessage();
                email.From = new MailAddress("application.assistant.bmw.11@gmail.com");
                email.To.Add(destinationEmail);
                email.Subject = subject;
                email.Body = message;
                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;

                //In case of sending files (Search Code)

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                string senderEmail = "application.assistant.bmw.11@gmail.com";
                string senderEmailPass = "phibuntqvaziimgs";
                smtp.Credentials = new System.Net.NetworkCredential(senderEmail, senderEmailPass);

                smtp.Send(email);
                Console.WriteLine("Email sended successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            // Obtén el usuario actual
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                // Verifica si se seleccionó un archivo
                if (file != null && file.Length > 0)
                {
                    // Convierte el archivo en una cadena base64
                    string profilePictureBase64;
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        byte[] fileBytes = memoryStream.ToArray();
                        profilePictureBase64 = Convert.ToBase64String(fileBytes);
                    }

                    // Actualiza la imagen de perfil en el modelo del usuario
                    user.ProfilePicture = profilePictureBase64;

                    // Guarda los cambios en la base de datos
                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        // El cambio de correo electrónico no fue exitoso, agrega errores de validación.
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View("ChangeProfilePicture");
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

    }
}

